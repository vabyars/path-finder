using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(string name, int[,] grid)
        {
            await context.Grids.AddAsync(new Grid
            {
                Name = name,
                Maze = grid
            });
            await context.SaveChangesAsync();
        }

        public async Task<int[,]> GetAsync(string name)
        {
            var maze = await context.Grids.FirstOrDefaultAsync(x => x.Name == name);
            return maze?.Maze;
        }
    }
}