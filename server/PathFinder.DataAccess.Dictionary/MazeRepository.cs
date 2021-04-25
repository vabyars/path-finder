using System;
using System.Collections.Generic;
using PathFinder.Domain.Interfaces;

namespace PathFinder.DataAccess.Dictionary
{
    public class MazeRepository: IMazeRepository
    {
        private readonly Dictionary<string, int[,]> _grids = new();
        
        public void Add(string name, int[,] grid)
        {
            _grids.Add(name, grid);
        }

        public int[,] Get(string name)
        {
            return _grids[name];
        }
    }
}