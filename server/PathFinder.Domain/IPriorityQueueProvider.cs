namespace PathFinder.Domain
{
    public interface IPriorityQueueProvider<T, out TQueue> where TQueue : IPriorityQueue<T>
    {
        public TQueue Create();
    }
}