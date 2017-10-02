using NUnit.Framework;
using System;
using System.Linq;

namespace OpenTSDBApiWrapper.DataTransformation.Fluent
{
    [TestFixture]
    public class MetricMapServiceTests
    {
        [Test]
        public void Map_WhenCalled_ReturnsExpectedValues()
        {
            var map = new ArticleOrderMetricMap();
            var sut = new MetricMapService();

            var order = new ArticleOrder
            {
                ArticleId = "1",
                AssortmentId = "100",
                Timestamp = DateTime.UtcNow.Date,
                Quantity = 2,
                Price = 200
            };

            var actual = sut.Map(map, new[] { order }).ToArray();

            foreach (var metric in actual)
            {
                AssertMetric(order, metric);
            }

            AssertMetric(actual[0], nameof(ArticleOrder.Quantity), 2.0);
            AssertMetric(actual[1], nameof(ArticleOrder.Price), 200.0);
        }

        [Test]
        public void Map_WhenCalledWithInvalidCharacters_ReturnsExpectedValues()
        {
            var map = new ArticleOrderMetricMap(
                quantityName: $"{nameof(ArticleOrder.Quantity)}$",
                priceName: $"{nameof(ArticleOrder.Price)}$"
            );
            var sut = new MetricMapService();

            var order = new ArticleOrder
            {
                ArticleId = "1$",
                AssortmentId = "100$",
                Timestamp = DateTime.UtcNow.Date,
                Quantity = 2,
                Price = 200
            };

            var actual = sut.Map(map, new[] { order }).ToArray();

            foreach (var metric in actual)
            {
                AssertMetric(order, metric, expectedValue: (_ => _.Replace("$", string.Empty)));
            }

            AssertMetric(actual[0], nameof(ArticleOrder.Quantity), 2.0);
            AssertMetric(actual[1], nameof(ArticleOrder.Price), 200.0);
        }

        [Test]
        public void Dispose_WhenCalledTwice_DoesNotThrow()
        {
            var map = new ArticleOrderMetricMap();

            Assert.That(() => map.Dispose(), Throws.Nothing);
            Assert.That(() => map.Dispose(), Throws.Nothing);
        }

        private void AssertMetric(ArticleOrder order, MetricData metric, Func<string, string> expectedValue = null)
        {
            expectedValue = expectedValue ?? (_ => _);

            Assert.That(metric.Timestamp, Is.GreaterThan(0));
            Assert.That(metric.Tags[nameof(ArticleOrder.ArticleId)], Is.EqualTo(expectedValue(order.ArticleId)));
            Assert.That(metric.Tags[nameof(ArticleOrder.AssortmentId)], Is.EqualTo(expectedValue(order.AssortmentId)));
        }

        private void AssertMetric(MetricData metric, string name, double value)
        {
            Assert.That(metric.Metric, Is.EqualTo(name));
            Assert.That(metric.Value, Is.EqualTo(value));
        }
    }
}
