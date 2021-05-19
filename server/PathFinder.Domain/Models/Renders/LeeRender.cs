using PathFinder.Domain.Models.Algorithms.Lee;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.StatisticsStates;

namespace PathFinder.Domain.Models.Renders
{
    public class LeeRender : Render
    {
        public LeeRender() : base(new[]{nameof(LeeAlgorithm)})
        {
        }

        protected override StatisticState CreateReportState(State resultState)
        {
            var state = base.CreateReportState(resultState);
            return new LeeStatisticState
            {
                IterationsCount = state.IterationsCount,
                PathLength = state.PathLength,
                Count = (resultState as LeeState)?.Cost
            };
        }
    }
}