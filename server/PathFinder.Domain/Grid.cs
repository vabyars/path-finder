using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain
{
    public class Grid : IGrid
    {
        private readonly int[,] _cells;

        private readonly int _width;

        private readonly int _height;

        private static readonly Point[] Directions =
        {
            new(1, 0),
            new(-1, 0),
            new(0, -1),
            new(0, 1),
        };

        private static readonly Point[] DirectionsWithDiagonal = Directions.Concat(new[]
        {
            new Point(1, 1),
            new(-1, 1),
            new(1, -1),
            new(-1, -1),
        }).ToArray();

        public Grid(int[,] cells)
        {
            _cells = cells;
            _width = cells.GetLength(0);
            _height = cells.GetLength(1);
        }

        public int this[int x, int y] //TODO add validation
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }

        public int this[Point p] //TODO add validation
        {
            get => _cells[p.X, p.Y];
            set => _cells[p.X, p.Y] = value;
        }

        public bool InBounds(Point point) => 0 <= point.X && point.X < _width && 0 <= point.Y && point.Y < _height;

        public bool IsPassable(Point point) => _cells[point.X, point.Y] >= 0;

        public IEnumerable<Point> GetNeighbors(Point point, bool allowDiagonal)
        {
            var source = allowDiagonal ? DirectionsWithDiagonal : Directions;

            foreach (var cell in source)
            {
                var next = new Point(point.X + cell.X, point.Y + cell.Y);
                if (InBounds(next) && IsPassable(next))
                    yield return next;
            }
        }
    }
}