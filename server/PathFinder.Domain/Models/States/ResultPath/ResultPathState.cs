using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Models.States.ResultPath
{
    public class ResultPathState : IState
    {
        public IEnumerable<Point> Path { get; set; } = new List<Point>();
    }
}