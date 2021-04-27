using System.Drawing;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Infrastructure
{
    public class DictionaryPriorityQueueProvider : IPriorityQueueProvider
    {
        public IPriorityQueue<Point> Create() => new DictionaryPriorityQueue<Point>();
    }
}