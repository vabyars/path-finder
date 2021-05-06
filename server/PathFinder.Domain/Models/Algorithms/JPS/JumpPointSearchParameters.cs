using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.JPS
{
    public class JumpPointSearchParameters : Parameters
    {
        public JumpPointSearchParameters(Point start, Point end, bool allowDiagonal) : base(start, end, allowDiagonal)
        {
        }
    }
}