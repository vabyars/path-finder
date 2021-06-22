using System.Drawing;
using PathFinder.Domain.Models.Algorithms.Realizations.JPS;

namespace PathFinder.Domain.Models.States
{
    public class InformativeState : IState
    {
        public Point CurrentPoint { get; set; }
        public JumpPointInformation JumpPointInformation { get; set; }
    }
}