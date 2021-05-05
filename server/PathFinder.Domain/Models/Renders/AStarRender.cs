using PathFinder.Domain.Models.Algorithms.AStar;

namespace PathFinder.Domain.Models.Renders
{
    public class AStarRender : Render
    {
        public AStarRender() : base(new[] {nameof(AStarAlgorithm)})
        {
        }
    }
}