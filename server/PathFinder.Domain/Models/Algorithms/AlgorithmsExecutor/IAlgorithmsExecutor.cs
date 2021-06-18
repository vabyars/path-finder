using System.Threading.Tasks;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;

namespace PathFinder.Domain.Models.Algorithms.AlgorithmsExecutor
{
    public interface IAlgorithmsExecutor
    {
        IAlgorithmReport Execute(IAlgorithm name, IGrid grid, IParameters parameters);
    }
}