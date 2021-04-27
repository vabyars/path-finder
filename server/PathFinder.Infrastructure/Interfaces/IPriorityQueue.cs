using System.Collections.Generic;

namespace PathFinder.Infrastructure.Interfaces
{
    public interface IPriorityQueue<TKey>
    {
        void Add(TKey key, double value);
        void Delete(TKey key);
        void Update(TKey key, double newValue);
        (TKey key, double value) ExtractMin();
        bool TryGetValue(TKey key, out double value);
        int Count { get; }
        IEnumerable<TKey> GetAllItems();
    }
    
    public static class PriorityQueueExtension
    {
        public static bool UpdateOrAdd<TKey>(this IPriorityQueue<TKey> queue, TKey node, double newValue)
        {
            var nodeInQueue = queue.TryGetValue(node, out var oldPrice);
            if (nodeInQueue && !(oldPrice > newValue)) return false;
            queue.Update(node, newValue);
            return true;
        }
    }
}