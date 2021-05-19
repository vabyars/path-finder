using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class TestGridToCheckShortestPath : TestGrid, IPath, IDiagonalPath
    {
        public override Grid Grid { get; } = new (new [,]
        {
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, -1, 1, 1},
            {1, -1, -1, -1, -1, -1, -1, -1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        });

        public override Point Start { get; } = new (0, 4);
        public override Point Goal { get; } = new(0, 8);
        public bool OnlyOneShortestPath { get; set; } = false;
        public int MinPathLength { get; set; } = 31;
        public IEnumerable<Point> MinPath { get; set; }
        public bool OnlyOneShortestDiagonalPath { get; set; } = false;
        public int MinPathLengthWithDiagonal { get; set; } = 25;
        public IEnumerable<Point> MinPathWithDiagonal { get; set; }
    }
}