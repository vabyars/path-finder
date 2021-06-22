using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1.Implementations
{
    public class MazeRepository: IMazeRepository
    {
        private readonly List<Grid> grids = new ();
        public void Add(Grid grid) => grids.Add(grid);

        public Grid Get(string name) => grids.FirstOrDefault(x => x.Name == name);

        public IEnumerable<string> GetMazesNames() => grids.Select(x => x.Name);
    }
}