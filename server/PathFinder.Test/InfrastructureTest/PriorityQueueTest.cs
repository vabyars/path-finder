using System;
using NUnit.Framework;
using PathFinder.Infrastructure.PriorityQueue;

namespace PathFinder.Test.InfrastructureTest
{
    public class PriorityQueueTest
    {
        public void Run(Func<IPriorityQueue<int>> getInstance)
        {
            EmptyWhenCreated(getInstance());
            QueueExtractMin(getInstance());
            QueueReturnMinDuringIteration(getInstance());
        }
        
        private void EmptyWhenCreated(IPriorityQueue<int> queue)
        {
            Assert.AreEqual(0, queue.Count);
        }
        
        private void QueueExtractMin(IPriorityQueue<int> queue)
        {
            queue.Add(1, 0);
            queue.Add(2, 1);
            queue.Add(3, 2);
            var (minKey, minValue) = queue.ExtractMin();
            while (queue.Count > 0)
            {
                var (key, value) = queue.ExtractMin();
                Assert.Less(minKey, key);
                Assert.Less(minValue, value);
                minKey = key;
                minValue = value;
            }
        }

        private void QueueReturnMinDuringIteration(IPriorityQueue<int> queue)
        {
            queue.Add(1, 0);
            queue.Add(2, 1);
            queue.Add(3, 2);
            var (minKey, _) = queue.ExtractMin();
            foreach (var key in queue)
            {
                Assert.Less(minKey, key);
                minKey = key;
            }
        }
    }
}