using System.Collections.Generic;

namespace PathFinder.Domain.Interfaces
{
    public interface IMazeService
    {
        IEnumerable<string> GetAvailableNames();
        void Add(string name, int[,] grid);
        int[,] Get(string name);
    }
}