using System.Text.RegularExpressions;

namespace OpenTSDBApiWrapper.Validators
{
    public class RegexMetricTextFilter : IMetricTextFilter
    {
        private static readonly Regex regex = new Regex(@"[^A-Za-z0-9/.\\_-]+", RegexOptions.Compiled);

        public string Filter(string value)
        {
            return string.IsNullOrEmpty(value) 
                ? value
                : regex.Replace(value, string.Empty);
        }
    }
}
