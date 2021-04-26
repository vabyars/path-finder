using System.Collections.Generic;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithmsExecutor
    {
        IEnumerable<string> AvailableAlgorithmNames();

        List<State> Execute(string name, IGrid grid, IParameters parameters);// изменить на RenderedStates
    }
}