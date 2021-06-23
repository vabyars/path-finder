using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Api.Models
{
    public record ExecuteAlgorithmRequest(
        string Name, 
        string Start, 
        string Goal, 
        bool AllowDiagonal,
        int[,] Grid,
        Metric Metric = Metric.Manhattan) { }
}