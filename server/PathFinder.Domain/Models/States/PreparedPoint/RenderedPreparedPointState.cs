using System.Drawing;

namespace PathFinder.Domain.Models.States.PreparedPoint
{
    public class RenderedPreparedPointState : RenderedState
    {
        public Color SecondColor { get; set; }
        public Point RenderedPoint { get; set; }
    }
}