using System;

namespace OpenTSDBApiWrapper.DataTransformation.Fluent
{
    public class ArticleOrder
    {
        public string ArticleId { get; set; }

        public string AssortmentId { get; set; }

        public DateTime Timestamp { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
