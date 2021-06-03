using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Infrastructure
{
    public class PriorityQueueProvider<T> : IPriorityQueueProvider<T>
    {
        public IPriorityQueue<T> Create()
        {
            return new HeapPriorityQueue<T>(); // TODO fix this hardcode
        }
    }
}