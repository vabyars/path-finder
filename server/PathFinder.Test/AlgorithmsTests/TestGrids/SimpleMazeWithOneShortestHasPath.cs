using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;


namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class SimpleMazeWithOneShortestHasPath : TestGrid, IHasPath, IHasDiagonalPath
    {
        public override Grid Grid { get; } = new(new[,]
        {
            {1, 1, 1, 1, 1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, 1, 1, -1, 1, 1}
        });

        public override Point Start => new (0, 0);
        public override Point Goal => new(5, 5);
        public bool OnlyOneShortestDiagonalPath { get; set; } = true;
        public int MinPathLengthWithDiagonal { get; set; } = 10;

        public IEnumerable<Point> MinPathWithDiagonal { get; set; } = new List<Point>
        {
            new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4),
            new(1, 5), new(2, 5), new(3, 5), new(4, 5), new(5, 5)
        };

        public bool OnlyOneShortestPath { get; set; } = true;
        public int MinPathLength { get; set; } = 11;

        public IEnumerable<Point> MinPath { get; set; } = new List<Point>
        {
            new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4), new(0, 5),
            new(1, 5), new(2, 5), new(3, 5), new(4, 5), new(5, 5)
        };
    }
}