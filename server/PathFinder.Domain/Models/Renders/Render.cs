using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.StatisticsStates;

namespace PathFinder.Domain.Models.Renders
{
    public abstract class Render
    {
        public string[] SupportingAlgorithms { get; }
        
        private int statesCount;

        private List<State> States { get; } = new();

        public Render(string[] algorithms)
        {
            SupportingAlgorithms = algorithms;
        }
        
        public virtual void RenderState(State state)
        {
            States.Add(state);
            statesCount++;
        }

        protected virtual StatisticState CreateReportState(State resultState)
        {
            return new()
            {
                IterationsCount = statesCount,
                PathLength = resultState.ResultPath.Count()
            };
        }

        public AlgorithmExecutionInfo GetInfo()
        {
            var resultState = States.TakeLast(1).FirstOrDefault();
            return new AlgorithmExecutionInfo
            {
                States = States.SkipLast(1),
                ResultPath = resultState?.ResultPath,
                Stat = CreateReportState(resultState),
            };
        }
    }
}