using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithmsExecutor
    {
        AlgorithmExecutionInfo Execute(IAlgorithm<State> name, IGrid grid, IParameters parameters);
    }
}