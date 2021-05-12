using System.Collections.Generic;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithmsExecutor
    {
        IEnumerable<string> AvailableAlgorithmNames();

        AlgorithmExecutionInfo Execute(string name, IGrid grid, IParameters parameters);// изменить на RenderedStates
    }
}