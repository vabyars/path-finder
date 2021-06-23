using System.Drawing;
using Moq;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.Realizations.JPS;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
using PathFinder.Infrastructure.PriorityQueue;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Test.AlgorithmsTests
{
    public class JpsTests
    {
        private JpsDiagonal algorithm;

        [SetUp]
        public void SetUp()
        {
            var pq = new Mock<IPriorityQueueProvider<Point, IPriorityQueue<Point>>>();
            pq.Setup(x => x.Create())
                .Returns(() => new HeapPriorityQueue<Point>());
            algorithm = new JpsDiagonal(new Mock<IRender>().Object, pq.Object);
        }
        
        [Test]
        public void TestOnUsualGrids()
        {
            new GridsTestController().TestOnUsualGrids(algorithm, true, true, Metric.Euclidean);
        }

        [Test]
        public void TestOnGridsWithoutWay()
        {
            new GridsTestController().TestOnGridsWithoutWay(algorithm);
        }
    }
}