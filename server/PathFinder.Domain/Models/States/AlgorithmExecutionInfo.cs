using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Models.States
{
    public class AlgorithmExecutionInfo
    {
        public IEnumerable<State> States { get; set; }
        
        public IEnumerable<Point> ResultPath { get; set; }
        
        public StatisticState Stat { get; set; }
    }
}