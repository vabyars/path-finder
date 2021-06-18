using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathFinder.Domain.Services.MazeService
{
    public interface IMazeService
    {
        IEnumerable<string> GetAvailableNames();
        void Add(string name, int[,] grid);
        int[,] Get(string name);
        Task AddAsync(string name, int[,] grid);
        Task<int[,]> GetAsync(string name);
        Task<IEnumerable<string>> GetAvailableNamesAsync();
    }
}