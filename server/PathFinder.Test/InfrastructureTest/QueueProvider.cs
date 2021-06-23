using System;
using NUnit.Framework;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Test.InfrastructureTest
{
    public class QueueProvider
    {
        [Test]
        public void Heap()
        {
            new PriorityQueueTest().Run(() => new HeapPriorityQueue<int>());
        }
        
        [Test]
        public void Dictionary()
        {
            new PriorityQueueTest().Run(() => new DictionaryPriorityQueue<int>());
        }
    }
}