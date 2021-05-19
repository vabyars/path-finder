using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.States.StatisticsStates;

namespace PathFinder.Domain.Models.States
{
    public class AlgorithmExecutionInfo
    {
        public IEnumerable<State> States { get; init; }

        public IEnumerable<Point> ResultPath { get; init; }

        public StatisticState Stat { get; init; }
    }
}