using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Models.States.ResultPath
{
    public class RenderedPathState : RenderedState
    {
        public IEnumerable<Point> Path { get; set; }
    }
}