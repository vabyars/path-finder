using System.Drawing;

namespace PathFinder.Infrastructure.Interfaces
{
    public interface IPriorityQueueProvider
    {
        IPriorityQueue<Point> Create();
    }
}