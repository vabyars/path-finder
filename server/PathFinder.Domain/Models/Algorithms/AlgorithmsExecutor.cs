using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        private readonly RenderProvider renderProvider;

        public AlgorithmsExecutor(RenderProvider renderProvider)
        {
            this.renderProvider = renderProvider;
        }

        public AlgorithmExecutionInfo Execute(IAlgorithm<State> algorithm, IGrid grid, IParameters parameters)
        {
            var render = renderProvider.GetRender(algorithm);
            var ex = algorithm.Run(grid, parameters);

            foreach (var state in ex)
            {
                render.RenderState(state);
            }
            return render.GetInfo();
        }
    }
}