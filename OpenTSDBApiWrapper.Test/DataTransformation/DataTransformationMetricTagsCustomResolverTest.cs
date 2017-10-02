using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using OpenTSDBApiWrapper.Exceptions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace OpenTSDBApiWrapper.Test {
    [TestFixture]
    public class DataTransformationMetricTagsCustomResolverTest {
        [SetUp]
        public void SetUp() {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private IFixture Fixture { get; set; }

        [TestCase(@" ")]
        [TestCase(@"=")]
        [TestCase(@"`")]
        [TestCase(@"[]|+}")]
        [TestCase(@"#*(*&^(")]
        [TestCase(@"|@#$")]
        [TestCase(@"|")]
        public void Source_With_Only_Invalid_Chars_Does_Not_Have_Any_Entry(string invalidChars) {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            Fixture.Customize<DataToBeConverteMultipleTag>(c => c
                .With(x => x.TagForMetric, invalidChars)
                .With(x => x.TagForMetric1, invalidChars));

            var obj = Fixture.Create<DataToBeConverteMultipleTag>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>());

            Assert.True(actual.Count == 0);
        }

        [TestCase(@" 01#*(abc*&^(01")]
        [TestCase(@"01abc*&^(01")]
        [TestCase(@"%!!@%^01#*(abc*&^(01")]
        [TestCase(@"01abc01")]
        public void Source_With_Valid_And_Invalid_Charaters_Removes_All_Invalid(string invalidChars) {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            Fixture.Customize<DataToBeConverteMultipleTag>(c => c
                .With(x => x.TagForMetric, invalidChars)
                .With(x => x.TagForMetric1, invalidChars));

            var obj = Fixture.Create<DataToBeConverteMultipleTag>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>());

            Assert.True(actual.Select(x => x.Value == "01abc01").Any());
        }

        [Test]
        public void Source_DoesNotContain_MetricTagAttribute_ThrowsMissingAttributeException() {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            var obj = Fixture.Create<MissingAttributeTagName>();

            Assert.Throws<MissingAttributeException>(() => sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>()));
        }

        [Test]
        public void Source_HasMultiple_Tags_Returns_Dictionary_With_Multiple() {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            var obj = Fixture.Create<DataToBeConverteMultipleTag>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>());

            Assert.True(actual.Count == 2);
        }

        [Test]
        public void Source_HasNull_ReturnsEmpty() {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            var obj = new DataToBeConvertedSingleTag {TagForMetric = null};

            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>());

            Assert.True(actual.Count == 0);
        }

        [Test]
        public void Source_HasSingleTag_Returns_OneTagDictionary() {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            var obj = Fixture.Create<DataToBeConvertedSingleTag>();
            var expected = new Dictionary<string, string> {{"opentag", obj.TagForMetric}};

            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>());

            Assert.True(expected.All(e => actual.Contains(e)));
        }

        [TestCase(@"metric name for test","MetricNameForTest")]
        [TestCase(@"This Is a test For Camel case","ThisIsATestForCamelCase")]
        public void Source_With_Spaces_Between_words_Replaces_with_CamelCased(string value,string expected)
        {
            var sut = Fixture.Freeze<MetricTagsCustomResolver>();
            Fixture.Customize<DataToBeConvertedSingleTag>(c => c
                .With(x => x.TagForMetric, value));

            var obj = Fixture.Create<DataToBeConvertedSingleTag>();
            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual(expected,actual["opentag"]);
        }
    }
}