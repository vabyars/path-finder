using System.Drawing;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Infrastructure;

namespace PathFinder.Test.AlgorithmsTests
{
    public class JpsTests
    {
        [Test]
        public void JpsTest()
        {
            new GridsTestController()
                .Run(new JpsDiagonal(new PriorityQueueProvider<Point>()), true, true, MetricName.Euclidean);
        }
    }
}