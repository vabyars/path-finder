using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public interface IDiagonalPath
    {
        bool OnlyOneShortestDiagonalPath { get; set; }
        int MinPathLengthWithDiagonal { get; set; }
        IEnumerable<Point> MinPathWithDiagonal { get; set; }
    }
}