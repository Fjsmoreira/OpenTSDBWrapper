using System;

namespace OpenTSDBApiWrapper.CustomAttributes
{
    public class IsMetricName : Attribute {
        public int Order { get; }
        public string Name { get; }

        public IsMetricName(string name, int order = 0) {
            Name = name;
            Order = order;
        }

        public IsMetricName(int order = 0) {
            Order = order;
        }
    }

}
