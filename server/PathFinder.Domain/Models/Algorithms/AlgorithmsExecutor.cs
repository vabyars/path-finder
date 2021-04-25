using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms.AStar;

namespace PathFinder.Domain.Models.Algorithms
{
    public class AlgorithmsExecutor : IAlgorithmsExecutor
    {
        private readonly IAlgorithm<IState, IParameters>[] _algorithms;

        public AlgorithmsExecutor(IAlgorithm<IState, IParameters>[] algorithms)
        {
            _algorithms = algorithms;
        }
        
        public IEnumerable<string> AvailableAlgorithmNames()
        {
            return _algorithms.Select(x => x.Name);
        }

        public List<IState> Execute(string name, IGrid grid, IParameters parameters)
        {
            var algorithm = _algorithms.FirstOrDefault(x => x.Name == name);
            if (algorithm == null)
                throw new ArgumentException($"algorithm not found: {name}");
            return algorithm.Run(grid, parameters).ToList();
        }
    }
}