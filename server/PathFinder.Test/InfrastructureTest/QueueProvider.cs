using System;
using NUnit.Framework;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Test.InfrastructureTest
{
    public class QueueProvider
    {
        [Test]
        public void Heap()
        {
            Func<HeapPriorityQueue<int>> getInstance = () => new HeapPriorityQueue<int>();
            new PriorityQueueTest().Run(getInstance);
        }
        
        [Test]
        public void Dictionary()
        {
            Func<DictionaryPriorityQueue<int>> getInstance = () => new DictionaryPriorityQueue<int>();
            new PriorityQueueTest().Run(getInstance);
        }
    }
}