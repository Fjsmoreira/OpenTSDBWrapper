using AutoMapper;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OpenTSDBApiWrapper
{
    public class OpenTsdbClient : IOpenTsdbClient
    {
        private static readonly RefitSettings refitSettings = new RefitSettings
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None
            }
        };

        private readonly HttpClient client;

        public OpenTsdbClient(HttpClient client)
        {
            this.client = client;

            AutoMapperCfg.Init();
        }

        public OpenTsdbClient(string url, bool useCompression = true)
        {
            if (useCompression)
            {
                client = new HttpClient(new GZipHttpMessageHandler());
            }
            else
            {
                client = new HttpClient();
            }

            client.BaseAddress = new Uri(url);

            AutoMapperCfg.Init();
        }

        public IAggregator Aggregator
            => GetService<IAggregator>();

        public IAnnotation Annotation
            => GetService<IAnnotation>();

        public IConfig Config
            => GetService<IConfig>();

        public IPutMetric Put
            => GetService<IPutMetric>();

        public IDropCaches DropCaches
            => GetService<IDropCaches>();

        public IQuery Query
            => GetService<IQuery>();

        public IRollup RollUp
            => GetService<IRollup>();

        private T GetService<T>()
            => RestService.For<T>(client, refitSettings);

        public MetricData Transform(object data) 
            => Mapper.Map<object, MetricData>(data);

        public IEnumerable<MetricData> TransformToMany(object data) 
            => Mapper.Map<object, IEnumerable<MetricData>>(data);
    }
}