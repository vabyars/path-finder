﻿using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class LargeSimpleGrid : TestGrid, IHasDiagonalPath, IHasPath
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

        public bool OnlyOneShortestDiagonalPath { get; } = true;
        public int MinPathLengthWithDiagonal { get; } = 15;

        public IEnumerable<Point> MinPathWithDiagonal { get; set; } = new List<Point>
        {
            new(0, 0), new(1, 1), new(2, 2), new(3, 3), new(4, 4), new(5, 5),
            new(6, 6), new(7, 7), new(8, 8), new(9, 9), new(10, 10), new(11, 11),
            new(12, 12), new(13, 13), new(14, 14)
        };

        public bool OnlyOneShortestPath { get; } = false;
        public int MinPathLength { get; } = 29;
        public IEnumerable<Point> MinPath { get; set; }
    }
}