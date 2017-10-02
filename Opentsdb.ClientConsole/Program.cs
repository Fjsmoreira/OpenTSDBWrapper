using OpenTSDBApiWrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opentsdb.ClientConsole
{
    class Program
    {
        private static readonly long epochTicks = new DateTime(1970, 1, 1).Ticks;

        static void Main(string[] args)
        {
            var tags = new Dictionary<string, string> { { "aaa", "a" }, { "ggg", "b" } };

            var clientUrl = "http://35.195.211.134:4242/";
            var client = new OpenTsdbClient(clientUrl);

            var metrics = Enumerable.Range(0, 100)
                .Select(_ => new MetricData
                {
                    Metric = "TestMetric123",
                    Tags = tags,
                    Timestamp = GetUnixTimeStamp(DateTime.UtcNow.AddMinutes(-_)),
                    Value = _
                })
                .ToArray();

            client.Put
                .Insert(metrics, true, 10000)
                .GetAwaiter()
                .GetResult();
        }

        private static long GetUnixTimeStamp(DateTime source)
        {
            return (source.Ticks - epochTicks) / TimeSpan.TicksPerSecond;
        }
    }
}
