﻿using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class SimpleMaze : TestGrid, IHasDiagonalPath, IHasPath
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
        public bool OnlyOneShortestDiagonalPath { get; } = true;
        public int MinPathLengthWithDiagonal { get; } = 9;

        public IEnumerable<Point> MinPathWithDiagonal { get; } = new List<Point>
        {
            new(0, 0), new(0, 1), new(1, 2), new(2, 2), new(3, 2), new(4, 2),
            new(5, 3), new(5, 4), new(5, 5)
        };

        public bool OnlyOneShortestPath { get; } = false;
        public int MinPathLength { get; } = 11;
        public IEnumerable<Point> MinPath { get; set; }
    }
}