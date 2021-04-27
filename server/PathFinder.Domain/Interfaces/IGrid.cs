using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.Domain.Interfaces
{
    public interface IGrid
    {
        bool InBounds(Point point);
        bool IsPassable(Point point);

        bool IsPassable(int x, int y);
        IEnumerable<Point> GetNeighbors(Point point, bool allowDiagonal);
        int this[Point point] { get; set; }
        int this[int x, int y] { get; set; }
    }
}