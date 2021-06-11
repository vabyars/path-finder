using System.Collections.Generic;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms
{
    public interface IAlgorithm
    {
        public IRender Render { get; }
        string Name { get; }
        IEnumerable<IState> Run(IGrid grid, IParameters parameters);
    }
}