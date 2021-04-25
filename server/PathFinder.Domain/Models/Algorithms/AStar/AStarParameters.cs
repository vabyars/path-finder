using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarParameters: Parameters
    {
        public AStarParameters(Point start, Point end, bool allowDiagonal) : base(start, end, allowDiagonal)
        {
        }
    }
}