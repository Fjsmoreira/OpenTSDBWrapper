using EnsureThat;
using OpenTSDBApiWrapper.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenTSDBApiWrapper.DataTransformation.Fluent
{
    public abstract class MetricMap<TSource> : IDisposable
        where TSource : class
    {
        private static readonly long epochTicks = new DateTime(1970, 1, 1).Ticks;

        private readonly IMetricTextFilter textFilter;

        private Func<TSource, DateTime> timestampSelector;

        private Dictionary<string, Func<TSource, string>> tagSelectors;
        private Dictionary<string, Func<TSource, double>> valueSelectors;

        protected MetricMap(IMetricTextFilter textFilter)
        {
            EnsureArg.IsNotNull(textFilter, nameof(textFilter));

            this.textFilter = textFilter;

            tagSelectors = new Dictionary<string, Func<TSource, string>>(StringComparer.OrdinalIgnoreCase);
            valueSelectors = new Dictionary<string, Func<TSource, double>>(StringComparer.OrdinalIgnoreCase);
        }

        protected void Timestamp(Expression<Func<TSource, DateTime>> selector)
        {
            EnsureArg.IsNotNull(selector, nameof(selector));

            timestampSelector = selector.Compile();
        }

        protected void Tag(Expression<Func<TSource, string>> selector, string name = null)
        {
            EnsureArg.IsNotNull(selector, nameof(selector));

            name = GetName(selector, name);

            EnsureArg.IsNotNullOrEmpty(name, nameof(name));

            tagSelectors[name] = selector.Compile();
        }

        protected void Value(Expression<Func<TSource, double>> selector, string name = null)
        {
            EnsureArg.IsNotNull(selector, nameof(selector));

            name = GetName(selector, name);

            EnsureArg.IsNotNullOrEmpty(name, nameof(name));

            valueSelectors[name] = selector.Compile();
        }

        private string GetName(LambdaExpression expression, string name)
            => textFilter.Filter(name ?? GetPropertyName(expression));

        internal IEnumerable<MetricData> Evaluate(TSource source)
        {
            EnsureArg.IsNotNull(source, nameof(source));

            var tags = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var tag in tagSelectors)
            {
                var tagValue = textFilter.Filter(tag.Value(source));

                if (!string.IsNullOrEmpty(tagValue))
                {
                    tags[tag.Key] = tagValue;
                }
            }

            var timestamp = GetUnixTimeStamp(timestampSelector(source));

            foreach (var value in valueSelectors)
            {
                var metricValue = value.Value(source);

                if (metricValue != 0.0)
                {
                    yield return new MetricData
                    {
                        Metric = value.Key,
                        Value = metricValue,
                        Timestamp = timestamp,
                        Tags = tags,
                    };
                }
            }
        }

        private static long GetUnixTimeStamp(DateTime source)
        {
            return (source.Ticks - epochTicks) / TimeSpan.TicksPerSecond;
        }

        private static string GetPropertyName(LambdaExpression lambda)
            => GetPropertyFromExpression(lambda)?.Name;

        private static PropertyInfo GetPropertyFromExpression(LambdaExpression lambda)
        {
            return
                ((lambda.Body as MemberExpression) ??
                ((lambda.Body as UnaryExpression)?.Operand as MemberExpression))?.Member as PropertyInfo;
        }

        public void Dispose()
        {
            timestampSelector = null;

            tagSelectors?.Clear();
            tagSelectors = null;

            valueSelectors?.Clear();
            valueSelectors = null;
        }
    }
}
