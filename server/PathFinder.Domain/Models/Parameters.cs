using System;
using System.Drawing;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;

namespace PathFinder.Domain.Models
{
    public class Parameters : IParameters // TODO подумать, как можно сделать норм полиморфизм с параметрами
    {                                       // м.б. написать свой ParametersParser
        public Point Start { get; }
        public Point End { get; }
        
        [AlgorithmSelectable("Diagonal", "allow", "not allow")]
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