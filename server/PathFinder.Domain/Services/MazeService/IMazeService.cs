using System.Collections.Generic;
using System.Threading.Tasks;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Domain.Services.MazeService
{
    public interface IMazeService
    {
        IEnumerable<string> GetAvailableNames();
        void Add(string name, GridWithStartAndEnd grid);
        GridWithStartAndEnd Get(string name);
        Task AddAsync(string name, GridWithStartAndEnd grid);
        Task<GridWithStartAndEnd> GetAsync(string name);
        Task<IEnumerable<string>> GetAvailableNamesAsync();
    }
}