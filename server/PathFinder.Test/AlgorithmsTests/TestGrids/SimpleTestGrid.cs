using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class SimpleTestGrid : TestGrid, IHasDiagonalPath, IHasPath
    {
        public override Grid Grid { get; } = new(new[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}});

        public override Point Start => new(0, 0);
        public override Point Goal => new(2, 2);

        public bool OnlyOneShortestPath { get; set; } = false;
        public int MinPathLength { get; set; } = 5;
        public IEnumerable<Point> MinPath { get; set; }
        
        public bool OnlyOneShortestDiagonalPath { get; set; } = true;
        public int MinPathLengthWithDiagonal { get; set; } = 3;
        public IEnumerable<Point> MinPathWithDiagonal { get; set; } = new[] {new Point(0, 0), new (1, 1), new(2, 2)};
    }
}