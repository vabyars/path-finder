using System.Collections.Generic;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Renders;

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