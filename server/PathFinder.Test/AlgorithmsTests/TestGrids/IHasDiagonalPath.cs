using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public interface IHasDiagonalPath
    {
        bool OnlyOneShortestDiagonalPath { get; }
        int MinPathLengthWithDiagonal { get; }
        IEnumerable<Point> MinPathWithDiagonal { get; }
    }
}