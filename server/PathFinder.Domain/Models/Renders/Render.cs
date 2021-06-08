using System;
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

        private List<RenderedState> States { get; } = new();

        public Render(string[] algorithms)
        {
            SupportingAlgorithms = algorithms;
        }
        
        public virtual void RenderState(State state)
        {
            States.Add(state switch
            {
                CurrentPointState s => new AStarRenderNew().RenderState(s),
                CandidateToPrepareState s => new AStarRenderNew().RenderState(s),
                ResultPathState s => new AStarRenderNew().RenderState(s),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            });
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
            return new()
            {
                RenderedStates = States
            };
        }
    }
}