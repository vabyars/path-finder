using System.Collections.Generic;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithm<out T> 
    {
        string Name { get; }
        IEnumerable<T> Run(IGrid grid, IParameters parameters);
    }
}