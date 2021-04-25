using System.Collections.Generic;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithmsExecutor
    {
        IEnumerable<string> AvailableAlgorithmNames();

        List<IState> Execute(string name, IGrid grid, IParameters parameters);// изменить на RenderedStates
    }
}