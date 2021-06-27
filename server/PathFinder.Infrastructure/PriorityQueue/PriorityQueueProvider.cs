using PathFinder.Domain;

namespace PathFinder.Infrastructure.PriorityQueue
{
    public class PriorityQueueProvider<T, TF> : IPriorityQueueProvider<T, TF>
        where TF : IPriorityQueue<T>, new()
    {
        public TF Create()
        {
            return new();
        }
    }
}