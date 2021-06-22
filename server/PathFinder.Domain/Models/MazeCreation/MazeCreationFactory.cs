using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.MazeCreation.MazeGenerators;
using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Domain.Models.MazeCreation
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

        public GridWithStartAndEnd Create(string name)
        {
            var generator = generators.FirstOrDefault(x => x.Name == name);
            return generator?.Create(parameters.Width, parameters.Height);
        }
    }
}