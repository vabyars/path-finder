using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathFinder.DataAccess1.Implementations
{
    public class MazeRepository: IMazeRepository
    {
        private readonly Dictionary<string, int[,]> grids = new();
        
        public IEnumerable<string> GetMazesNames() => grids.Keys;

        public void Add(string name, int[,] grid)
        {
            grids.Add(name, grid);
        }

        public int[,] Get(string name)
        {
            return grids[name];
        }
        
        public async Task AddAsync(string name, int[,] grid)
        {
            await Task.Run(() => grids.Add(name, grid));
        }

        public async Task<int[,]> GetAsync(string name)
        {
            return await Task.Run(() => grids[name]);
        }
        
        public async Task<IEnumerable<string>> GetMazesNamesAsync()
        {
            return await Task.Run(() => grids.Keys);
        }
    }
}