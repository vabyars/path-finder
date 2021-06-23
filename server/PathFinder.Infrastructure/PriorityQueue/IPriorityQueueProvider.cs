namespace PathFinder.Infrastructure.PriorityQueue
{
    public interface IPriorityQueueProvider<T, out TF> where TF : IPriorityQueue<T>
    {
        public TF Create();
    }
}