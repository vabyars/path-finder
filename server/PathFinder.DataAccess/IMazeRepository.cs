using System.Collections.Generic;
using PathFinder.DataAccess.Entities;
using System.Threading.Tasks;

namespace PathFinder.DataAccess
{
    public interface IMazeRepository
    {
        IEnumerable<string> GetMazesNames();
        void Add(Grid grid);
        Grid Get(string name);
        Task AddAsync(Grid grid);
        Task<Grid> GetAsync(string name);
        Task<IEnumerable<string>> GetMazesNamesAsync();
    }
}