using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.States
{
    public class State : IState
    {
        public IEnumerable<Point> Points { get; init; }
        public Point? Point { get; init; }
    }
}