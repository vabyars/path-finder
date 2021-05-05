using System.Collections.Generic;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public abstract class Render
    {
        public string[] SupportingAlgorithms { get; }
        
        private int _statesCount;

        public List<State> States { get; } = new();

        public Render(string[] algorithms)
        {
            SupportingAlgorithms = algorithms;
        }
        
        public virtual void RenderState(State state)
        {
            //некоторые манипуляции со стейтами
            States.Add(state);
            _statesCount++;
        }

        public virtual void CreateReportState()
        {
            States.Add(new StatisticState
            {
                IterationsCount = _statesCount
            });
        }
    }
}