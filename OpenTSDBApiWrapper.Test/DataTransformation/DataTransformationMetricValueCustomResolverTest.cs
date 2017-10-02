using AutoMapper;
using Moq;
using NUnit.Framework;
using OpenTSDBApiWrapper.Exceptions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace OpenTSDBApiWrapper.Test {
    [TestFixture]
    public class DataTransformationMetricValueCustomResolverTest {
        [SetUp]
        public void SetUp() {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private IFixture Fixture { get; set; }

        [Test]
        public void Source_DoesNotContain_MetricIsTimeAttribute_ThrowsMissingAttributeException() {
            var sut = Fixture.Freeze<MetricValueCustomResolver>();
            var obj = Fixture.Create<MissingAttributeIsValue>();

            Assert.Throws<MissingAttributeException>(() => sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<double>(), It.IsAny<ResolutionContext>()));
        }

        [Test]
        public void Source_HasMultiple_TimeAttributes_Returns_First() {
            var sut = Fixture.Freeze<MetricValueCustomResolver>();
            var obj = Fixture.Create<DataToBeConvertedMultipleValues>();
            var expected = obj.Value;

            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<double>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Source_HasSingleIsTimeAttribute_Returns_UnixTimestampLong() {
            var sut = Fixture.Freeze<MetricValueCustomResolver>();
            var obj = Fixture.Create<DataToBeConvertedSingleValue>();
            var expected = obj.Value;

            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<double>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual(expected, actual);
        }
    }
}