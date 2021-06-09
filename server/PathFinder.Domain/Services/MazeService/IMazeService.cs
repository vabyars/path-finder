using System.Collections.Generic;

namespace PathFinder.Domain.Services.MazeService
{
    public interface IMazeService
    {
        IEnumerable<string> GetAvailableNames();
        void Add(string name, int[,] grid);
        int[,] Get(string name);
    }
}