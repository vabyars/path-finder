using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;

namespace PathFinder.Domain.Models.Algorithms.AlgorithmsExecutor
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        public IAlgorithmReport Execute(IAlgorithm algorithm, IGrid grid, IParameters parameters)
        {
            var render = algorithm.Render;
            var ex = algorithm.Run(grid, parameters);

            foreach (var state in ex)
            {
                render.RenderState(state);
            }

            return render.GetReport();
        }
    }
}