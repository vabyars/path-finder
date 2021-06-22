using System.Collections.Generic;
using System.Drawing;
using AutoMapper;
using PathFinder.DataAccess1.Entities;
using System.Threading.Tasks;

namespace PathFinder.Domain.Services.MazeService
{
    public interface IMazeService
    {
        IEnumerable<string> GetAvailableNames();
        void Add(string name, GridWithStartAndEnd grid);
        GridWithStartAndEnd Get(string name);
        Task AddAsync(string name, GridWithStartAndEnd grid);
        Task<GridWithStartAndEnd> GetAsync(string name);
        Task<IEnumerable<string>> GetAvailableNamesAsync();
    }
    
    public class GridWithStartAndEnd
    {
        public int[,] Maze { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
    }
    
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Grid, GridWithStartAndEnd>()
                .ReverseMap();
        }

    }
}