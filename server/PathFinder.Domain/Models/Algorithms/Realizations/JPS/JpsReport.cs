using System.Collections.Generic;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms.Realizations.JPS
{
    public class JpsReport : IAlgorithmReport
    {
        public List<RenderedState> RenderedStates { get; set; }
        public int PathLength { get; set; }
        public int PointsPrepared { get; set; }
    }
}