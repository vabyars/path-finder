using System.Drawing;

namespace PathFinder.Domain.Interfaces
{
    public interface IParameters
    {
        Point Start { get; }
        Point End { get; }
        bool AllowDiagonal { get; } 
    }
}