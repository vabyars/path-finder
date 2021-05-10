using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.States
{
    public class State : IState
    {
        public IEnumerable<Point> Points { get; set; }
        public Point? Point { get; set; }
    }
}