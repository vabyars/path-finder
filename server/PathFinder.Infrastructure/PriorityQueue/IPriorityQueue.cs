using System.Collections.Generic;

namespace PathFinder.Infrastructure.PriorityQueue
{
    public interface IPriorityQueue<TKey> : IEnumerable<TKey>
    {
        void Add(TKey key, double value);
        void Delete(TKey key);
        void Update(TKey key, double newValue);
        (TKey key, double value) ExtractMin();
        bool TryGetValue(TKey key, out double value);
        int Count { get; }
    }
    
    public static class PriorityQueueExtension
    {
        public static bool UpdateOrAdd<TKey>(this IPriorityQueue<TKey> queue, TKey node, double newValue)
        {
            var nodeInQueue = queue.TryGetValue(node, out var oldPrice);
            if (nodeInQueue && !(oldPrice > newValue)) 
                return false;
            if (nodeInQueue)
                queue.Update(node, newValue);
            else
                queue.Add(node, newValue);
            return true;
        }
    }
}