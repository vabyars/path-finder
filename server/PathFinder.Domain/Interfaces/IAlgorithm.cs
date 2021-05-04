using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithm<out T> 
    {
        string Name { get; }
        IEnumerable<T> Run(IGrid grid, IParameters parameters);

        IEnumerable<Point> GetResultPath();
    }
}