using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class TestGridToCheckShortestHasPath : TestGrid, IHasPath, IHasDiagonalPath
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
        public bool OnlyOneShortestPath { get; } = false;
        public int MinPathLength { get; } = 31;
        public IEnumerable<Point> MinPath { get; set; }
        public bool OnlyOneShortestDiagonalPath { get; } = false;
        public int MinPathLengthWithDiagonal { get; } = 25;
        public IEnumerable<Point> MinPathWithDiagonal { get; set; }
    }
}