using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        public IAlgorithmReport Execute(IAlgorithm algorithm, IGrid grid, IParameters parameters)
        {
            var render = ((AStarAlgorithm) algorithm).Render;
            var ex = algorithm.Run(grid, parameters);

            foreach (var state in ex)
            {
                render.RenderState(state);
            }

            return render.GetReport();
        }
    }
}