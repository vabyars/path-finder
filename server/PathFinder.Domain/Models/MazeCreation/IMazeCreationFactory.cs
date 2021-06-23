using System.Collections.Generic;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Domain.Models.MazeCreation
{
    public interface IMazeCreationFactory
    {
        IEnumerable<string> GetAvailableNames();
        GridWithStartAndEnd Create(string name);
    }
}