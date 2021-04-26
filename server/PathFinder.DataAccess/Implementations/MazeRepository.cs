using System.Collections.Generic;

namespace PathFinder.DataAccess1.Implementations
{
    public class MazeRepository: IMazeRepository
    {
        private readonly Dictionary<string, int[,]> _grids = new();
        
        public void Add(string name, int[,] grid)
        {
            _grids.Add(name, grid);
        }

        public int[,] Get(string name)
        {
            return _grids[name];
        }

        public IEnumerable<string> GetMazesNames() => _grids.Keys;

        public bool TryGetValue(string name, out int[,] value)
        {
            return _grids.TryGetValue(name, out value);
        }
    }
}