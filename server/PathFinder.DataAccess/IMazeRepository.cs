using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathFinder.DataAccess1
{
    public interface IMazeRepository
    {
        IEnumerable<string> GetMazesNames();
        void Add(string name, int[,] grid);
        int[,] Get(string name);
        
        Task AddAsync(string name, int[,] grid);
        Task<int[,]> GetAsync(string name);
    }
}