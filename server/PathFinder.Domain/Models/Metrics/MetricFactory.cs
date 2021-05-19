using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PathFinder.Domain.Models.Metrics
{
    public class MetricFactory : IMetricFactory
    {
        private static readonly Dictionary<MetricName, Func<Point, Point, double>> Euristics = new()
        {
            {MetricName.Euclidean, EuclideanMetric},
            {MetricName.Manhattan, (from, to) => Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y)}
        };

        public IEnumerable<string> GetAvailableMetricNames() => Euristics.Keys.Select(x => x.ToString());
        
        public Func<Point, Point, double> GetMetric(MetricName name)
        {
            return !Euristics.TryGetValue(name, out var metric) ? null : metric;
        }

        private static double EuclideanMetric(Point from, Point to)
        {
            var x = from.X - to.X;
            var y = from.Y - to.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}