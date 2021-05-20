using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public interface IHasPath
    {
        bool OnlyOneShortestPath { get; }
        int MinPathLength { get; }
        IEnumerable<Point> MinPath { get; }
    }
}