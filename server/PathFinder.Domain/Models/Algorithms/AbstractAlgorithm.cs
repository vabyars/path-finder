using System.Collections.Generic;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms
{
    public abstract class AbstractAlgorithm : IAlgorithm
    {
        public IRender Render { get; }

        protected AbstractAlgorithm(IRender render)
        {
            Render = render;
        }
        
        public abstract string Name { get; }
        public abstract IEnumerable<IState> Run(IGrid grid, IParameters parameters);
    }
}