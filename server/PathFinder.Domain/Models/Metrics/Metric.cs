using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Models.Metrics
{
    public enum Metric
    {
        Euclidean,
        Manhattan
    }

    public static class MetricExtensions
    {
        private static readonly Dictionary<Metric, Func<Point, Point, double>> Heuristics = new()
        {
            {Metric.Euclidean, EuclideanMetric},
            {Metric.Manhattan, (from, to) => Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y)}
        };

        private static readonly string[] Names;
        static MetricExtensions()
        {
            Names = Enum.GetNames(typeof(Metric));
        }
        
        public static double Call(this Metric metric, Point from, Point to)
            => Heuristics[metric](from, to);

        public static string[] AvailableNames() => Names;

        private static double EuclideanMetric(Point from, Point to)
        {
            var x = from.X - to.X;
            var y = from.Y - to.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}