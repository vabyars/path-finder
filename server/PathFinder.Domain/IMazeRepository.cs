using System.Collections.Generic;
using System.Threading.Tasks;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Domain
{
    public interface IMazeRepository
    {
        IEnumerable<string> GetMazesNames();
        void Add(string name, GridWithStartAndEnd grid);
        GridWithStartAndEnd Get(string name);
        Task AddAsync(string name, GridWithStartAndEnd grid);
        Task<GridWithStartAndEnd> GetAsync(string name);
        Task<IEnumerable<string>> GetMazesNamesAsync();
    }
}