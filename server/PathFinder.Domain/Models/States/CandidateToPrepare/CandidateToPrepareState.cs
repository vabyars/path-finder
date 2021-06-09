using System.Drawing;

namespace PathFinder.Domain.Models.States.CandidateToPrepare
{
    public class CandidateToPrepareState : IState
    {
        public Point Candidate { get; set; }
    }
}