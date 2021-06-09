using System.Collections.Generic;

namespace PathFinder.Domain.Models.MazeCreation
{
    public interface IMazeCreationFactory
    {
        IEnumerable<string> GetAvailableNames();
        int[,] Create(string name);
    }
}