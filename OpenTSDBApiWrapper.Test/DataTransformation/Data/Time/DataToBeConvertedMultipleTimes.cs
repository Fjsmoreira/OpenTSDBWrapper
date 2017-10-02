using System;
using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedMultipleTimes {
        [IsMetricTime]
        public DateTime Time => new DateTime(2017, 03, 24, 02, 0, 0, DateTimeKind.Utc);

        [IsMetricTime]
        public DateTime Time1 => new DateTime(2017, 07, 24, 02, 0, 0, DateTimeKind.Utc);

        [IsMetricTime]
        public DateTime Time2 => new DateTime(2017, 09, 24, 02, 0, 0, DateTimeKind.Utc);
    }
}