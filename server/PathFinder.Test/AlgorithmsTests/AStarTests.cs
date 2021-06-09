using System.Drawing;
using Moq;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Test.AlgorithmsTests
{
    [TestFixture]
    public class AStarTests
    {
        [Test]
        public void AStarTest()
        {
            var pq = new Mock<IPriorityQueueProvider<Point>>();
            pq.Setup(x => x.Create())
                .Returns(() => new HeapPriorityQueue<Point>());
            new GridsTestController() 
                .Run(new AStarAlgorithm(new Mock<IRender>().Object, pq.Object), true, false, MetricName.Euclidean);
        }
    }
}