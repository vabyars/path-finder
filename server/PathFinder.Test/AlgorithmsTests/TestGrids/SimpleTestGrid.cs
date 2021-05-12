using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public abstract class TestGrid
    {
        public abstract Grid Grid { get; }
        public abstract Point Start { get; }
        public abstract Point Goal { get; }
    }

    public class SimpleMaze : TestGrid, IDiagonalPath, IPath
    {
        public override Grid Grid { get; } = new(new[,]
        {
            {1, 1, 1, 1, 1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, -1, 1, -1, -1, 1},
            {1, 1, 1, 1, 1, 1}
        });

        public override Point Start => new (0, 0);
        public override Point Goal => new(5, 5);
        public bool OnlyOneShortestDiagonalPath { get; set; } = true;
        public int MinPathLengthWithDiagonal { get; set; } = 9;

        public IEnumerable<Point> MinPathWithDiagonal { get; set; } = new List<Point>
        {
            new(0, 0), new(0, 1), new(1, 2), new(2, 2), new(3, 2), new(4, 2),
            new(5, 3), new(5, 4), new(5, 5)
        };

        public bool OnlyOneShortestPath { get; set; } = false;
        public int MinPathLength { get; set; } = 11;
        public IEnumerable<Point> MinPath { get; set; }
    }

    public class LargeSimpleGrid : TestGrid, IDiagonalPath, IPath
    {
        public override Grid Grid { get; } = new(new [,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        });

        public override Point Start => new(0, 0);
        public override Point Goal => new(14, 14);

        public bool OnlyOneShortestDiagonalPath { get; set; } = true;
        public int MinPathLengthWithDiagonal { get; set; } = 15;

        public IEnumerable<Point> MinPathWithDiagonal { get; set; } = new List<Point>()
        {
            new(0, 0), new(1, 1), new(2, 2), new(3, 3), new(4, 4), new(5, 5),
            new(6, 6), new(7, 7), new(8, 8), new(9, 9), new(10, 10), new(11, 11),
            new(12, 12), new(13, 13), new(14, 14)
        };

        public bool OnlyOneShortestPath { get; set; } = false;
        public int MinPathLength { get; set; } = 29;
        public IEnumerable<Point> MinPath { get; set; }
    }
    public class SimpleTestGrid : TestGrid, IDiagonalPath, IPath
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

    public interface IDiagonalPath
    {
        bool OnlyOneShortestDiagonalPath { get; set; }
        int MinPathLengthWithDiagonal { get; set; }
        IEnumerable<Point> MinPathWithDiagonal { get; set; }
    }

    public interface IPath
    {
        bool OnlyOneShortestPath { get; set; }
        int MinPathLength { get; set; }
        IEnumerable<Point> MinPath { get; set; }
    }
}