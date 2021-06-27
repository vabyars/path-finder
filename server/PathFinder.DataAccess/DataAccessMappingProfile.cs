using AutoMapper;
using PathFinder.Domain.Models.GridFolder;
using Grid = PathFinder.DataAccess.Entities.Grid;

namespace PathFinder.DataAccess
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