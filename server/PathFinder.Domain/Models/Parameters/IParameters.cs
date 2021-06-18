using System;
using System.Drawing;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Domain.Models.Parameters
{
    public interface IParameters
    {
        Point Start { get; }
        Point End { get; }
        bool AllowDiagonal { get; }
        Metric Metric { get; }
    }
}