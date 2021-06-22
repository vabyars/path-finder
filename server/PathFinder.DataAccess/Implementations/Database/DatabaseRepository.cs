using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1.Implementations.Database
{
    public class DatabaseRepository : IMazeRepository
    {
        private readonly MazeContext context;

        public DatabaseRepository(MazeContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> GetMazesNames() => context.Grids.Select(x => x.Name);

        public void Add(Grid grid)
        {
            if (context.Grids.Any(x => x.Name == grid.Name))
                throw new ArgumentException($"maze with name {grid.Name} already exists");
            context.Grids.Add(grid);
            context.SaveChanges();
        }

        public Grid Get(string name) => context.Grids.FirstOrDefault(x => x.Name == name);
    }
}