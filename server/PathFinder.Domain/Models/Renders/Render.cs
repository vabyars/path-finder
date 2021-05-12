using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public abstract class Render
    {
        public string[] SupportingAlgorithms { get; }
        
        private int _statesCount;

        private List<State> States { get; } = new();

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

        public AlgorithmExecutionInfo GetInfo()
        {
            return new()
            {
                States = States.SkipLast(1),
                ResultPath = States.TakeLast(1).FirstOrDefault()?.Points,
                Stat = new StatisticState
                {
                    IterationsCount = _statesCount
                }
            };
        }
    }
}