using AutoMapper;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Services.MazeService;
using Grid = PathFinder.DataAccess.Entities.Grid;

namespace PathFinder.Domain
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Grid, GridWithStartAndEnd>()
                .ReverseMap();
        }

    }
}