using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;

namespace PathFinder.Domain.Models.Algorithms.AlgorithmsExecutor
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        public IAlgorithmReport Execute(IAlgorithm algorithm, IGrid grid, IParameters parameters)
        {
            var render = algorithm.Render; // TODO подумать над другим способом получения рендера

            foreach (var state in algorithm.Run(grid, parameters))
                render.RenderState(state);

            return render.GetReport();
        }
    }
}