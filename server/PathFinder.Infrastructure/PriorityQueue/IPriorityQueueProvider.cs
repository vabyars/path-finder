namespace PathFinder.Infrastructure.PriorityQueue
{
    public interface IPriorityQueueProvider<T>
    {
        IPriorityQueue<T> Create();
    }
}