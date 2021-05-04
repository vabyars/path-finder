using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        private readonly IAlgorithm<State>[] _algorithms;

        public AlgorithmsExecutor(IAlgorithm<State>[] algorithms) 
        {
            _algorithms = algorithms;
        }
        
        public IEnumerable<string> AvailableAlgorithmNames()
            => _algorithms.Select(x => x.Name);

        public List<State> Execute(string name, IGrid grid, IParameters parameters)
        {
            var render = new Render();
            var algorithm = _algorithms.FirstOrDefault(x => x.Name == name);
            if (algorithm == null)
                throw new ArgumentException($"algorithm not found: {name}");
            var ex = algorithm.Run(grid, parameters);
            
            foreach (var state in ex)
            {
                render.RenderState(state);
            }

            render.CreateReportState();
            return render.States;
        }
    }
}