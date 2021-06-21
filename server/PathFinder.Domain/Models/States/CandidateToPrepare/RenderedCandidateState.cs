using System.Drawing;

namespace PathFinder.Domain.Models.States.CandidateToPrepare
{
    public class RenderedCandidateState : RenderedState
    {
        public string SecondColor { get; set; }
        public Point RenderedPoint { get; set; }
    }
}