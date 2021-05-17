using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Test.AlgorithmsTests
{
    public class JPSTests
    {
        [Test]
        public void JPSTest()
        {
            new GridsTestController()
                .Run(q => new JpsDiagonal(q), true, MetricName.Euclidean);
        }
    }
}