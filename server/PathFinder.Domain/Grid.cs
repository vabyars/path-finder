using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain
{
    [Serializable]
    public class Grid : IGrid
    {
        private readonly int[,] _cells;

        [JsonIgnore]
        private readonly int _width;

        [JsonIgnore]
        private readonly int _height;

        private static readonly Point[] Directions =
        {
            new(1, 0),
            new(-1, 0),
            new(0, -1),
            new(0, 1),
        };

        private static readonly Point[] DiagonalDirections = {
            new(1, 1),
            new(-1, 1),
            new(1, -1),
            new(-1, -1),
        };

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

        public bool InBounds(Point point) => InBounds(point.X, point.Y);

        public bool InBounds(int x, int y) => 0 <= x && x < _width && 0 <= y && y < _height;
        
        public bool IsPassable(Point point) => IsPassable(point.X, point.Y);

        public bool IsPassable(int x, int y) => InBounds(x, y) && _cells[x, y] >= 0;
        
        public double GetCost(Point from, Point to)
        {
            var cost = _cells[to.X, to.Y];
            if (Directions.Contains(new Point(from.X - to.X, from.Y - to.Y)))
                return cost;
            return cost * Math.Sqrt(2);
        }
        
        public IEnumerable<Point> GetNeighbors(Point point, bool allowDiagonal)
        {
            foreach (var cell in GetPointsWithOffset(point, Directions))
                yield return cell;

            if (!allowDiagonal)
                yield break;
            
            foreach (var cell in GetPointsWithOffset(point, DiagonalDirections))
                yield return cell;
        }

        private IEnumerable<Point> GetPointsWithOffset(Point point, IEnumerable<Point> offsets)
        {
            return offsets
                .Select(cell => new Point(point.X + cell.X, point.Y + cell.Y))
                .Where(next => InBounds(next) && IsPassable(next));
        }
    }
}