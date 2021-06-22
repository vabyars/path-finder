using System.Drawing;
using Moq;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.Realizations.AStar;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
using PathFinder.Infrastructure.PriorityQueue;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Test.AlgorithmsTests
{
    [TestFixture]
    public class AStarTests
    {
        [Test]
        public void AStarTest()
        {
            var pq = new Mock<IPriorityQueueProvider<Point, IPriorityQueue<Point>>>();
            pq.Setup(x => x.Create())
                .Returns(() => new HeapPriorityQueue<Point>());
            new GridsTestController() 
                .Run(new AStarAlgorithm(new Mock<IRender>().Object, pq.Object), true, false, Metric.Euclidean);
        }
    }
}