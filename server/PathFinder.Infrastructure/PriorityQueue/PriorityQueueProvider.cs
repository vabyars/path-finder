using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Infrastructure.PriorityQueue
{
    public class PriorityQueueProvider<T> : IPriorityQueueProvider<T>
    {
        public IPriorityQueue<T> Create()
        {
            return new HeapPriorityQueue<T>(); // TODO fix this hardcode
        }
    }
}