using System.Collections.Generic;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public class Render
    {
        private int _statesCount;

        public List<State> States { get; } = new();
        public void RenderState(State state)
        {
            //некоторые манипуляции со стейтами
            States.Add(state);
            _statesCount++;
        }

        public void CreateReportState()
        {
            States.Add(new StatisticState
            {
                IterationsCount = _statesCount
            });
        }
    }
}