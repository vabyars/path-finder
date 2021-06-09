using System.Drawing;
using Moq;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Test.AlgorithmsTests
{
    public class JpsTests
    {
        [Test]
        public void JpsTest() // не проходят потому что надо написать стейты в самом алгосе
        {
            var pq = new Mock<IPriorityQueueProvider<Point>>();
            pq.Setup(x => x.Create())
                .Returns(() => new HeapPriorityQueue<Point>());
            new GridsTestController()
                .Run(new JpsDiagonal(new Mock<IRender>().Object, pq.Object), true, true, MetricName.Euclidean);
        }
    }
}