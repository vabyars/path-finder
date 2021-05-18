using System;
using System.Drawing;

namespace PathFinder.Domain.Models.Algorithms.JPS
{
    public class JPSParameters : Parameters
    {
        public JPSParameters(Point start, Point end, bool allowDiagonal, Func<Point, Point, double> metric) : base(start, end, allowDiagonal, metric)
        {
        }
        
        public new bool AllowDiagonal { get; set; }
    }
}