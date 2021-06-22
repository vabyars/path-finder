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
        private AStarAlgorithm algorithm;

        [SetUp]
        public void SetUp()
        {
            var pq = new Mock<IPriorityQueueProvider<Point, IPriorityQueue<Point>>>();
            pq.Setup(x => x.Create())
                .Returns(() => new HeapPriorityQueue<Point>());
            algorithm = new AStarAlgorithm(new Mock<IRender>().Object, pq.Object);
        }
        
        [Test]
        public void AStarTest()
        {
            new GridsTestController().TestOnUsualGrids(algorithm, true, false, Metric.Euclidean);
        }

        [Test]
        public void TestOnGridWithoutWay()
        {
            new GridsTestController().TestOnGridsWithoutWay(algorithm);
        }
    }
}