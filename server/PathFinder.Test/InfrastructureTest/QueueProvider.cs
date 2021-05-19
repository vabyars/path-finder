using System;
using NUnit.Framework;
using PathFinder.Infrastructure;

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