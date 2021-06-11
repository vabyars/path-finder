using System;
using System.Drawing;

namespace PathFinder.Domain.Models.Parameters
{
    public class Parameters : IParameters
    {                                       
        public Point Start { get; }
        public Point End { get; }
        
        public bool AllowDiagonal { get; }
        
        public Func<Point, Point, double> Metric { get; }

        public Parameters(Point start, Point end, bool allowDiagonal, Func<Point, Point, double> metric)
        {
            Start = start;
            End = end;
            AllowDiagonal = allowDiagonal;
            Metric = metric;
        }
    }
}