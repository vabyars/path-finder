using System.Drawing;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarState : State
    {
        public Point Point { get; }

        public AStarState(Point point)
        {
            Point = point;
        }
    }
}