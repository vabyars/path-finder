using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        private readonly IAlgorithm<State>[] algorithms;
        private readonly RenderProvider renderProvider;

        public AlgorithmsExecutor(IAlgorithm<State>[] algorithms, RenderProvider renderProvider)
        {
            this.algorithms = algorithms;
            this.renderProvider = renderProvider;
        }
        
        public IEnumerable<string> AvailableAlgorithmNames()
            => algorithms.Select(x => x.Name);

        public AlgorithmExecutionInfo Execute(string name, IGrid grid, IParameters parameters)
        {
            var algorithm = algorithms.FirstOrDefault(x => x.Name == name);
            if (algorithm == null)
                throw new ArgumentException($"algorithm not found: {name}");
            
            var render = renderProvider.GetRender(algorithm);
            var ex = algorithm.Run(grid, parameters);

            foreach (var state in ex)
            {
                render.RenderState(state);
            }
            render.CreateReportState();
            return render.GetInfo();
        }
    }
}