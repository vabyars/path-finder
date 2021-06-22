using System.Drawing;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Infrastructure.PriorityQueue
{
    /*public class PriorityQueueProvider<T> : IPriorityQueueProvider<T>
    {
        public IPriorityQueue<T> Create()
        {
            return new HeapPriorityQueue<T>(); // TODO fix this hardcode
        }
    }*/

    public interface IPriorityQueueProvider<T, out TF> where TF : IPriorityQueue<T>
    {
        public TF Create();
    }

    public class PriorityQueueProvider<T, TF> : IPriorityQueueProvider<T, TF>
        where TF : IPriorityQueue<T>, new()
    {
        public TF Create()
        {
            return new();
        }
    }
}