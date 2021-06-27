using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PathFinder.Domain;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.DataAccess.Implementations.Database
{
    public class DatabaseRepository : IMazeRepository
    {
        private readonly MazeContext context;
        private readonly IMapper mapper;

        public DatabaseRepository(MazeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<string> GetMazesNames() => context.Grids.Select(x => x.Name);

        public void Add(string name, GridWithStartAndEnd grid)
        {
            var newGrid = mapper.Map<Entities.Grid>(grid);
            newGrid.Name = name;
            context.Grids.Add(newGrid);
            context.SaveChanges();
        }

        public GridWithStartAndEnd Get(string name)
        {
            var grid =  context.Grids.FirstOrDefault(x => x.Name == name);
            return mapper.Map<GridWithStartAndEnd>(grid);
        }

        public async Task AddAsync(string name, GridWithStartAndEnd grid)
        {
            var newGrid = mapper.Map<Entities.Grid>(grid);
            newGrid.Name = name;
            await context.Grids.AddAsync(newGrid);
            await context.SaveChangesAsync();
        }

        public async Task<GridWithStartAndEnd> GetAsync(string name)
        {
            var grid = await context.Grids.FirstOrDefaultAsync(x => x.Name == name);
            return mapper.Map<GridWithStartAndEnd>(grid);
        }
        
        public async Task<IEnumerable<string>> GetMazesNamesAsync()
        {
            return await context.Grids.Select(x => x.Name).ToListAsync();
        }
    }
}