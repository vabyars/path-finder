using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models
{
    public class MazeCreationFactoryTestRealization : IMazeCreationFactory
    {
        private readonly IMazeGenerator[] _generators; 
        public MazeCreationFactoryTestRealization(IMazeGenerator[] generators)
        {
            _generators = generators;
        }
        
        public IEnumerable<string> GetAvailableNames()
            => _generators.Select(x => x.Name);

        public int[,] Create(string name)
        {
            var generator = _generators.FirstOrDefault(x => x.Name == name);
            if (generator == null)
                throw new ArgumentException($"cannot find creator with name \"{name}\"");
            return generator.Create();
        }
    }
}