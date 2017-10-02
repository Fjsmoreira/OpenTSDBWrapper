using System.Globalization;
using System.Text.RegularExpressions;

namespace OpenTSDBApiWrapper
{
    public static class ValidatorHelper
    {
        private static readonly Regex ValidCharactersRegex = new Regex(@"[^A-Za-z0-9/.\\_-]+", RegexOptions.Compiled);

        public static string TagAndMetricNameValidator(string value)
            => ValidCharactersRegex.Replace(value, string.Empty);

        public static string ToTitleCase(string str) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }
}
