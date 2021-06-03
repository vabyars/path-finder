using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models
{
    public class MazeCreationFactory : IMazeCreationFactory
    {
        private readonly IEnumerable<IMazeGenerator> generators;
        private readonly GridConfigurationParameters parameters;

        public MazeCreationFactory(IEnumerable<IMazeGenerator> generators, GridConfigurationParameters parameters)
        {
            this.generators = generators;
            this.parameters = parameters;
        }
        
        public IEnumerable<string> GetAvailableNames()
            => generators.Select(x => x.Name);

        public int[,] Create(string name)
        {
            var generator = generators.FirstOrDefault(x => x.Name == name);
            if (generator == null)
                throw new ArgumentException($"cannot find creator with name \"{name}\"");
            return generator.Create(parameters.Width, parameters.Height);
        }
    }
}