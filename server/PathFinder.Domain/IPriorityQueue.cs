using System.Collections.Generic;

namespace PathFinder.Domain
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
            switch (nodeInQueue)
            {
                case true when !(oldPrice > newValue):
                    return false;
                case true:
                    queue.Update(node, newValue);
                    break;
                default:
                    queue.Add(node, newValue);
                    break;
            }

            return true;
        }
    }
}