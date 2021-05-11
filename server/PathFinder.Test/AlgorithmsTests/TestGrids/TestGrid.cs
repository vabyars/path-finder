using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class TestGrid
    {
        //add findsMinPath: bool

        public Grid Grid { get; set; } = new(new[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}});

        public Point Start { get; set; } = new(0, 0);
        public Point Goal { get; set; } = new(2, 2);

        public bool OnlyOneShortestPath { get; set; } = false;
        public bool OnlyOneShortestDiagonalPath { get; set; } = true;
        
        public int MinPathLengthWithDiagonal { get; set; } = 3;
        public int MinPathLengthWithoutDiagonal { get; set; } = 5;

        public IEnumerable<Point> MinPathWithDiagonal { get; set; } = new[] {new Point(0, 0), new (1, 1), new(2, 2)};
    }
}