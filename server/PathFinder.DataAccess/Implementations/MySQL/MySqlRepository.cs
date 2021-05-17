using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1.Implementations.MySQL
{
    public class MySqlRepository : IMazeRepository
    {
        private readonly MazeContext context;

        public MySqlRepository(MazeContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> GetMazesNames()
        {
            return context.Grids.Select(x => x.Name);
        }

        public void Add(string name, int[,] grid)
        {
            if (context.Grids.Any(x => x.Name == name))
                throw new ArgumentException($"maze with name {name} already exists");
            context.Grids.Add(new Grid
            {
                Name = name,
                Maze = grid
            });
            context.SaveChanges();
        }

        public int[,] Get(string name)
        {
            return context.Grids.FirstOrDefault(x => x.Name == name)?.Maze;
        }

        public bool TryGetValue(string name, out int[,] value)
        {
            value = Get(name);
            return value != null;
        }
    }
}