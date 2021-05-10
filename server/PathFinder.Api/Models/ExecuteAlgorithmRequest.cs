using PathFinder.Domain;
using PathFinder.Domain.Metrics;

namespace PathFinder.Api.Models
{
    public class ExecuteAlgorithmRequest
    {
        public string Name { get; set; }
        public string Start { get; set; }
        public string Goal { get; set; }
        public bool AllowDiagonal { get; set; }

        public Metric MetricName { get; set; } = Metric.Euclidean;
        public int[,] Grid { get; set; }
    }
}