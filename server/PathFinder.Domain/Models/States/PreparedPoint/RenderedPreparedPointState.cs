using System.Drawing;

namespace PathFinder.Domain.Models.States.PreparedPoint
{
    public class RenderedPreparedPointState : RenderedState
    {
        public string SecondColor { get; set; }
        public Point RenderedPoint { get; set; }
    }
}