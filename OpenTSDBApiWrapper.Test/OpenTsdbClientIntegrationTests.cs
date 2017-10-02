using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace OpenTSDBApiWrapper.Test.DataTransformation
{
    [TestFixture]
    public class OpenTsdbClientIntegrationTests
    {
        [Test, Ignore("IntegrationTest")]
        public void Transform()
        {
            var data = new Fixture().CreateMany<PerformanceStorageData>(10000).ToList();
            var stopWatch = new Stopwatch();

            for (var i = 0; i <= 1; i++)
            {
                data.AddRange(new Fixture().CreateMany<PerformanceStorageData>(10000).ToList());
            }
            stopWatch.Start();

            var sut = new OpenTsdbClient(new HttpClient());
            var metricData = new ConcurrentBag<IEnumerable<MetricData>>();

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            };

            Parallel.ForEach(data, options, d => {
                metricData.Add(sut.TransformToMany(d));
            });

            stopWatch.Stop();
            var metric = stopWatch.Elapsed;            
        }
    }
}
