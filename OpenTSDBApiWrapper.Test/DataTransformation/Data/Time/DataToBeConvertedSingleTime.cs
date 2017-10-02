using System;
using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedSingleTime {
        [IsMetricTime]
        public DateTime Time => new DateTime(2017,03,24,02,0,0,DateTimeKind.Utc);

    }
}