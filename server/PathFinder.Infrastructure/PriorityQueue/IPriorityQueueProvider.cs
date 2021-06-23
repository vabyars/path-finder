namespace PathFinder.Infrastructure.PriorityQueue
{
    public interface IPriorityQueueProvider<T, out TQueue> where TQueue : IPriorityQueue<T>
    {
        public TQueue Create();
    }
}