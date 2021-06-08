using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States.StatisticsStates;

namespace PathFinder.Domain.Models.States
{
    public class AlgorithmExecutionInfo
    {
        public IEnumerable<RenderedState> RenderedStates { get; init; }
    }
}