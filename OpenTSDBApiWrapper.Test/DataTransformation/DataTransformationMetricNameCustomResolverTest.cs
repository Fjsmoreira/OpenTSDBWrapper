using AutoMapper;
using Moq;
using NUnit.Framework;
using OpenTSDBApiWrapper.Exceptions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace OpenTSDBApiWrapper.Test {
    [TestFixture]
    public class DataTransformationMetricNameCustomResolverTest {
        [SetUp]
        public void SetUp() {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private IFixture Fixture { get; set; }

        [TestCase(".")]
        public void Source_HasMultiple_Names_Returns_Values_Separated_By_Separator_Value(string separator) {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            var missingMetricName = Fixture.Create<DataToBeConvertedMultipleMetricNamesOrdered>();
            var expected = $"{missingMetricName.NameOfMetric1}{separator}" +
                           $"{missingMetricName.NameOfMetric2}{separator}" +
                           $"{missingMetricName.NameOfMetric3}";

            Assert.AreEqual(expected, sut.Resolve(missingMetricName, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>()));
        }

        [TestCase(".")]
        public void Source_HasMultiple_Names_Returns_ValueFromObject_Ordered_By_Attribute_Order(string separator) {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            var missingMetricName = Fixture.Create<DataToBeConvertedMultipleMetricNamesUnordered>();
            var expected = $"{missingMetricName.NameOfMetric3}{separator}" +
                           $"{missingMetricName.NameOfMetric1}{separator}" +
                           $"{missingMetricName.NameOfMetric2}";

            Assert.AreEqual(expected, sut.Resolve(missingMetricName, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>()));
        }

        [TestCase(".")]
        public void Source_HasMultipleNames_Without_Order_Returns_By_ParameterName_Order(string separator) {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            var missingMetricName = Fixture.Create<DataToBeConvertedNoOrderInMetricNames>();
            var expected = $"{missingMetricName.NameOfMetric1}{separator}" +
                           $"{missingMetricName.NameOfMetric2}{separator}" +
                           $"{missingMetricName.NameOfMetric3}";

            Assert.AreEqual(expected, sut.Resolve(missingMetricName, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>()));
        }

        [TestCase(@" ")]
        [TestCase(@"=")]
        [TestCase(@"`")]
        [TestCase(@"[]|+}")]
        [TestCase(@"#*(*&^(")]
        [TestCase(@"|@#$")]
        [TestCase(@"|")]
        public void Source_With_Only_Invalid_Chars_Has_Empty_Metric(string invalidChars) {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            Fixture.Customize<DataToBeConvertedSingleMetricName>(c => c
                .With(x => x.NameOfMetric, invalidChars));

            var obj = Fixture.Create<DataToBeConvertedSingleMetricName>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual("", actual);
        }

        [TestCase(@" 01#*(abc*&^(01")]
        [TestCase(@"01abc*&^(01")]
        [TestCase(@"%!!@%^01#*(abc*&^(01")]
        [TestCase(@"01abc01")]
        public void Source_With_Valid_And_Invalid_Charaters_Removes_All_Invalid(string invalidChars) {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            Fixture.Customize<DataToBeConvertedSingleMetricName>(c => c
                .With(x => x.NameOfMetric, invalidChars));

            var obj = Fixture.Create<DataToBeConvertedSingleMetricName>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual("01abc01", actual);
        }

        [Test]
        public void Source_DoesNotContain_MetricNameAttribute_ThrowsMissingAttributeException() {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            var missingMetricName = Fixture.Create<MissingAttributeMetricName>();

            Assert.Throws<MissingAttributeException>(() => sut.Resolve(missingMetricName, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>()));
        }

        [Test]
        public void Source_HasSingleName_Returns_ValueFromObject() {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            var missingMetricName = Fixture.Create<DataToBeConvertedSingleMetricName>();

            Assert.AreEqual(missingMetricName.NameOfMetric, sut.Resolve(missingMetricName, It.IsAny<MetricData>(), It.IsAny<string>(), It.IsAny<ResolutionContext>()));
        }

        [TestCase(@"metric name for test", "MetricNameForTest")]
        [TestCase(@"This Is a test For Camel case", "ThisIsATestForCamelCase")]
        public void Source_With_Spaces_Between_words_Replaces_with_CamelCased(string value, string expected)
        {
            var sut = Fixture.Freeze<MetricNameCustomResolver>();
            Fixture.Customize<DataToBeConvertedSingleMetricName>(c => c
                .With(x => x.NameOfMetric, value));

            var obj = Fixture.Create<DataToBeConvertedSingleMetricName>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(),It.IsAny<string>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual(expected,actual);
        }
    }
}