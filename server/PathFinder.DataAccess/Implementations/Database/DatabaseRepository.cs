using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PathFinder.DataAccess.Entities;

namespace PathFinder.DataAccess.Implementations.Database
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
        
        public async Task AddAsync(Grid grid)
        {
            await context.Grids.AddAsync(grid);
            await context.SaveChangesAsync();
        }

        public async Task<Grid> GetAsync(string name)
        {
            return await context.Grids.FirstOrDefaultAsync(x => x.Name == name);
        }
        
        public async Task<IEnumerable<string>> GetMazesNamesAsync()
        {
            return await context.Grids.Select(x => x.Name).ToListAsync();
        }
    }
}