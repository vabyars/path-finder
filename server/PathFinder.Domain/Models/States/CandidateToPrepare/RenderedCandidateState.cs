using System.Drawing;

namespace PathFinder.Domain.Models.States.CandidateToPrepare
{
    public class RenderedCandidateState : RenderedState
    {
        public Color SecondColor { get; set; }
        public Point RenderedPoint { get; set; }
    }
}