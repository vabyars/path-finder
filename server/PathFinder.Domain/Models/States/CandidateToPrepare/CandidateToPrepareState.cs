using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.States.CandidateToPrepare
{
    public class CandidateToPrepareState : IState
    {
        public Point Candidate { get; set; }
    }
}