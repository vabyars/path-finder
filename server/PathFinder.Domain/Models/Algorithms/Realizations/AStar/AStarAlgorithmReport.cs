using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms.Realizations.AStar
{
    public class AlgorithmReport : IAlgorithmReport
    {
        public List<RenderedState> RenderedStates { get; }
        public RenderedState Result { get; }
        public AlgorithmReport(IReadOnlyCollection<RenderedState> states)
        {
            RenderedStates = states.SkipLast(1).ToList();
            Result = states.TakeLast(1).First();
        }
    }
}