
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Ploeh.AutoFixture;

namespace OpenTSDBApiWrapper.Test.DataTransformation
{
    [TestFixture]
    public class TestOfDataTransformationTime
    {
        [Test]
        public void Transform() {
            var data = new Fixture().CreateMany<PerformanceStorageData>(1000).ToList();
            var stopWatch = new Stopwatch();

            for (var i = 0; i <= 2; i++) {
                data.AddRange(new Fixture().CreateMany<PerformanceStorageData>(1000).ToList());
            }
            stopWatch.Start();
            var sut = new OpenTsdbClient(new HttpClient());
            var metricData = new List<MetricData>();
            foreach (var performanceStorageData in data) {
                metricData.AddRange(sut.TransformToMany(performanceStorageData));
            }
            stopWatch.Stop();
            var metric = stopWatch.Elapsed;


        }
    }
}
