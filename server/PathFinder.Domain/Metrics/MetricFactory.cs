using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Metrics
{
    public static class MetricFactory
    {
        private static readonly Dictionary<Metric, Func<Point, Point, double>> Euristics = new()
        {
            {Metric.Euclidean, EuclideanMetric},
            {Metric.Manhattan, (from, to) => Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y)}
        };

        public static Func<Point, Point, double> GetMetric(Metric name)
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