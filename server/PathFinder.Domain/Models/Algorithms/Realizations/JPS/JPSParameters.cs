using System;
using System.Drawing;

namespace PathFinder.Domain.Models.Algorithms.Realizations.JPS
{
    public class JpsParameters : Parameters.Parameters
    {
        public JpsParameters(Point start, Point end, bool allowDiagonal, Func<Point, Point, double> metric) 
            : base(start, end, allowDiagonal, metric)
        {
        }
    }
}