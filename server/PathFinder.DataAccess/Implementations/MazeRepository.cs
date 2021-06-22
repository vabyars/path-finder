using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1.Implementations
{
    public class MazeRepository: IMazeRepository
    {

        private readonly List<Grid> grids = new ();
        public void Add(Grid grid) => grids.Add(grid);

        public Grid Get(string name) => grids.FirstOrDefault(x => x.Name == name);

        public IEnumerable<string> GetMazesNames() => grids.Select(x => x.Name);
        
        public async Task AddAsync(Grid grid) => await Task.Run(() => grids.Add(grid));

        public async Task<Grid> GetAsync(string name) => await Task.Run(() => grids.FirstOrDefault(x => x.Name == name));

        public async Task<IEnumerable<string>> GetMazesNamesAsync() => await Task.Run(() => grids.Select(x => x.Name));
    }
}