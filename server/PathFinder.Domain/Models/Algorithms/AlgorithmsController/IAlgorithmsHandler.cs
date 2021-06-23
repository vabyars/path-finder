using System.Collections.Generic;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;

namespace PathFinder.Domain.Models.Algorithms.AlgorithmsController
{
    public interface IAlgorithmsHandler
    {
        IEnumerable<string> GetAvailableAlgorithmNames();
        IAlgorithmReport ExecuteAlgorithm(string name, IGrid grid, IParameters parameters);
    }
}