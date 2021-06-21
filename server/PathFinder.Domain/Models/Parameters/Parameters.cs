using System;
using System.Drawing;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Domain.Models.Parameters
{
    public class Parameters : IParameters
    {                                       
        public Point Start { get; }
        public Point End { get; }
        
        public bool AllowDiagonal { get; }
        
        public Metric Metric { get; }

        public Parameters(Point start, Point end, bool allowDiagonal, Metric metric)
        {
            Start = start;
            End = end;
            AllowDiagonal = allowDiagonal;
            Metric = metric;
        }
    }
}