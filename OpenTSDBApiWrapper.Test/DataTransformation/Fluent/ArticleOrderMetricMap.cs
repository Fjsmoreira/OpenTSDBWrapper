using OpenTSDBApiWrapper.Validators;

namespace OpenTSDBApiWrapper.DataTransformation.Fluent
{
    public class ArticleOrderMetricMap : MetricMap<ArticleOrder>
    {
        public ArticleOrderMetricMap(
            string quantityName = null,
            string priceName = null) 
            : base(new RegexMetricTextFilter())
        {
            Timestamp(_ => _.Timestamp);

            Tag(_ => _.ArticleId);
            Tag(_ => _.AssortmentId);

            Value(_ => _.Quantity, quantityName);
            Value(_ => _.Price, priceName);
        }
    }
}
