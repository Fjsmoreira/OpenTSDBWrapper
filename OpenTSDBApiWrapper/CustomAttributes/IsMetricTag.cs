using System;

namespace OpenTSDBApiWrapper.CustomAttributes {
    public class IsMetricTag : Attribute
    {
        public string Name { get;}

        public IsMetricTag(string name) {
            Name = name;
        }

    }
}