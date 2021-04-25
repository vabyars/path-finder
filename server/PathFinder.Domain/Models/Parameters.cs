using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models
{
    public abstract class Parameters : IParameters
    {
        public Point Start { get; }
        public Point End { get; }
        public bool AllowDiagonal { get; }

        protected Parameters(Point start, Point end, bool allowDiagonal)
        {
            Start = start;
            End = end;
            AllowDiagonal = allowDiagonal;
        }
    }
}