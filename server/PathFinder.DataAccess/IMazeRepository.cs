using System.Collections.Generic;

namespace PathFinder.DataAccess1
{
    public interface IMazeRepository
    {
        IEnumerable<string> GetMazesNames();
        void Add(string name, int[,] grid);
        int[,] Get(string name);
        bool TryGetValue(string name, out int[,] value);
    }
}