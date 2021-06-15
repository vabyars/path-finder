﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.Domain.Models.Algorithms.AlgorithmsExecutor;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;

namespace PathFinder.Domain.Models.Algorithms.AlgorithmsController
{
    public class AlgorithmsHandler
    {
        private readonly IEnumerable<IAlgorithm> algorithms;
        private readonly IAlgorithmsExecutor algorithmsExecutor;

        public AlgorithmsHandler(IEnumerable<IAlgorithm> algorithms, IAlgorithmsExecutor algorithmsExecutor)
        {
            this.algorithms = algorithms;
            this.algorithmsExecutor = algorithmsExecutor;
        }

        public IEnumerable<string> GetAvailableAlgorithmNames() => algorithms.Select(x => x.Name);

        public async Task<IAlgorithmReport> ExecuteAlgorithm(string name, IGrid grid, IParameters parameters)
        {
            var algorithm = algorithms.FirstOrDefault(x => x.Name == name);
            if (algorithm == null)
                throw new ArgumentException($"algorithm not found: {name}");
            return await algorithmsExecutor.Execute(algorithm, grid, parameters);
        }
    }
}