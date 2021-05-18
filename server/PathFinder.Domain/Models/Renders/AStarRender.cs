using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Algorithms.Lee;

namespace PathFinder.Domain.Models.Renders
{
    public class AStarRender : Render // TODO fix!!!!!!!!!!!
    {
        public AStarRender() : base(new[] {nameof(AStarAlgorithm), nameof(JpsDiagonal), nameof(LeeAlgorithm), nameof(IDA)})
        {
        }
    }
}