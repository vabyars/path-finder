using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.Domain;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.DataAccess.Implementations
{
    public class MazeRepository: IMazeRepository
    {
        private readonly Dictionary<string, GridWithStartAndEnd> grids = new();
        public void Add(string name, GridWithStartAndEnd grid) => grids.Add(name, grid);

        public GridWithStartAndEnd Get(string name) => grids[name];

        public IEnumerable<string> GetMazesNames() => grids.Keys;
        
        public async Task AddAsync(string name, GridWithStartAndEnd grid) => await Task.Run(() => Add(name, grid));

        public async Task<GridWithStartAndEnd> GetAsync(string name) => await Task.Run(() => Get(name));

        public async Task<IEnumerable<string>> GetMazesNamesAsync() => await Task.Run(GetMazesNames);
    }
}