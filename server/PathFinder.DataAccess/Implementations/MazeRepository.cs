using System.Collections.Generic;

namespace PathFinder.DataAccess1.Implementations
{
    public class MazeRepository: IMazeRepository
    {
        private readonly Dictionary<string, int[,]> grids = new();
        
        public void Add(string name, int[,] grid)
        {
            grids.Add(name, grid);
        }

        public int[,] Get(string name)
        {
            return grids.TryGetValue(name, out var res) ? res : null;
        }

        public IEnumerable<string> GetMazesNames() => grids.Keys;
    }
}