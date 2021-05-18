using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public interface IPath
    {
        bool OnlyOneShortestPath { get; set; }
        int MinPathLength { get; set; }
        IEnumerable<Point> MinPath { get; set; }
    }
}