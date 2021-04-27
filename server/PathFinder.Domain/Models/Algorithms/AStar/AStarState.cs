using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarState : State
    {
        public string Name { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public Point? Point { get; }

        public AStarState(string name)
        {
            Name = name;
        }
        public AStarState(Point point, string name) : this(name)
        {
            Point = point;
        }

        public AStarState(IEnumerable<Point> points, string name) : this(name)
        {
            Points = points;
        }
    }
}