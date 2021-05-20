using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Algorithms.JPS;

namespace PathFinder.Domain.Models.Renders
{
    public class AStarRender : Render
    {
        public AStarRender() : base(new[] {nameof(AStarAlgorithm), nameof(JpsDiagonal), nameof(IDA)})
        {
        }
    }
}