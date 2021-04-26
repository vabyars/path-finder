using System.Collections.Generic;

namespace PathFinder.Domain.Interfaces
{
    public interface IMazeCreationFactory
    {
        IEnumerable<string> GetAvailableNames();
        int[,] Create(string name);
    }
}