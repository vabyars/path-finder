using System.Collections.Generic;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithm<out T, in TP> 
        where T: IState
        where TP: IParameters
    {
        string Name { get; }
        IEnumerable<T> Run(IGrid grid, TP parameters);
    }
}