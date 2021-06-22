using System.Drawing;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Domain.Models.Parameters
{
    public record Parameters(Point Start, Point End, bool AllowDiagonal, Metric Metric) : IParameters { }
}