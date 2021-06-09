using System;
using System.Drawing;
using PathFinder.Domain.Models.Algorithms;

namespace PathFinder.Domain.Models.Parameters
{
    public class Parameters : IParameters // TODO подумать, как можно сделать норм полиморфизм с параметрами
    {                                       // м.б. написать свой ParametersParser
        public Point Start { get; }
        public Point End { get; }
        
        [AlgorithmSelectableParameter("Diagonal")]
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