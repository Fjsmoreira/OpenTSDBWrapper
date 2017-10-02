using System.Collections.Generic;
using AutoMapper;

namespace OpenTSDBApiWrapper
{
    public static class AutoMapperCfg
    {
        public static void Init() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<object, MetricData>()
                   .ForMember(dest => dest.Metric, opt => opt.ResolveUsing<MetricNameCustomResolver>())
                   .ForMember(dest => dest.Tags, opt => opt.ResolveUsing<MetricTagsCustomResolver>())
                   .ForMember(dest => dest.Value, opt => opt.ResolveUsing<MetricValueCustomResolver>())
                   .ForMember(dest => dest.Timestamp, opt => opt.ResolveUsing<MetricTimeCustomResolver>());

                cfg.CreateMap<object, IEnumerable<MetricData>>().ConstructUsing(x => new ConvertToListOfMetricsResolver().Resolve(x, null, x, null));

            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
