using System.Drawing;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms.JPS
{
    public class JumpPointSearchState : State
    {
        public Point Point { get; }
        
        public JumpPointSearchState(Point point)
        {
            Point = point;
        }
    }
}