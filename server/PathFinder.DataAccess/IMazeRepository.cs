using System.Collections.Generic;
using System.Drawing;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1
{
    public interface IMazeRepository
    {
        IEnumerable<string> GetMazesNames();
        void Add(Grid grid);
        Grid Get(string name);
    }
}