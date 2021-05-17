using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models
{
    public class MazeCreationFactory : IMazeCreationFactory
    {
        private readonly IMazeGenerator[] _generators;
        private readonly GridConfigurationParameters _parameters;

        public MazeCreationFactory(IMazeGenerator[] generators, GridConfigurationParameters parameters)
        {
            _generators = generators;
            _parameters = parameters;
        }
        
        public IEnumerable<string> GetAvailableNames()
            => _generators.Select(x => x.Name);

        public int[,] Create(string name)
        {
            var generator = _generators.FirstOrDefault(x => x.Name == name);
            if (generator == null)
                throw new ArgumentException($"cannot find creator with name \"{name}\"");
            return generator.Create(_parameters.Width, _parameters.Height);
        }
    }
}