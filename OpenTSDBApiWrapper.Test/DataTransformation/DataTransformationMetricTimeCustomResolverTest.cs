using AutoMapper;
using Moq;
using NUnit.Framework;
using OpenTSDBApiWrapper.Exceptions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace OpenTSDBApiWrapper.Test {
    [TestFixture]
    public class DataTransformationMetricTimeCustomResolverTest {
        [SetUp]
        public void SetUp() {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private IFixture Fixture { get; set; }

        [Test]
        public void Source_DoesNotContain_MetricIsTimeAttribute_ThrowsMissingAttributeException() {
            var sut = Fixture.Freeze<MetricTimeCustomResolver>();
            var obj = Fixture.Create<MissingAttributeIsTime>();

            Assert.Throws<MissingAttributeException>(() => sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<long>(), It.IsAny<ResolutionContext>()));
        }

        [Test]
        public void Source_HasMultiple_TimeAttributes_Returns_First() {
            var sut = Fixture.Freeze<MetricTimeCustomResolver>();
            var obj = new DataToBeConvertedMultipleTimes();
            long expected = 1490320800; // 03/24/2017 @ 2:00am (UTC)

            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<long>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Source_HasSingleIsTimeAttribute_Returns_UnixTimestampLong() {
            var sut = Fixture.Freeze<MetricTimeCustomResolver>();
            var obj = new DataToBeConvertedSingleTime();
            long expected = 1490320800; // 03/24/2017 @ 2:00am (UTC)

            var actual = sut.Resolve(obj, It.IsAny<MetricData>(), It.IsAny<long>(), It.IsAny<ResolutionContext>());

            Assert.AreEqual(expected, actual);
        }
    }
}