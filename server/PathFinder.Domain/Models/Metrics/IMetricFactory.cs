using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Models.Metrics
{
    public interface IMetricFactory
    {
        IEnumerable<string> GetAvailableMetricNames();
        Func<Point, Point, double> GetMetric(MetricName name);
    }
}