using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithmsExecutor
    {
        IAlgorithmReport Execute(IAlgorithm name, IGrid grid, IParameters parameters);
    }
}