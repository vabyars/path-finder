using System.Drawing;

namespace PathFinder.Domain.Models.States
{
    public class RenderedInformativeState : RenderedState
    {
        public string Color { get; set; }
        
        public string SecondColor { get; set; }
        
        public Point RenderedPoint { get; set; }
    }
}