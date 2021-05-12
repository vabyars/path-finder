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
        private readonly IAlgorithm<State>[] _algorithms;
        private readonly RenderProvider _renderProvider;

        public AlgorithmsExecutor(IAlgorithm<State>[] algorithms, RenderProvider renderProvider)
        {
            _algorithms = algorithms;
            _renderProvider = renderProvider;
        }
        
        public IEnumerable<string> AvailableAlgorithmNames()
            => _algorithms.Select(x => x.Name);

        public AlgorithmExecutionInfo Execute(string name, IGrid grid, IParameters parameters)
        {
            var algorithm = _algorithms.FirstOrDefault(x => x.Name == name);
            if (algorithm == null)
                throw new ArgumentException($"algorithm not found: {name}");
            
            var render = _renderProvider.GetRender(algorithm);
            var ex = algorithm.Run(grid, parameters);

            foreach (var state in ex)
            {
                render.RenderState(state);
            }

            return render.GetInfo();
        }
    }

    public class RenderProvider
    {
        private readonly Render[] _renders;

        public RenderProvider(Render[] renders)
        {
            _renders = renders;
        }
        
        public Render GetRender(IAlgorithm<State> algorithm)
        {
            var algorithmName = algorithm.GetType().Name;
            var render = _renders.FirstOrDefault(x => x.SupportingAlgorithms.Contains(algorithmName));
            if (render == null)
                throw new ArgumentException($"{algorithmName} has not render");
            return render;
        }
    }
}