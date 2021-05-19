using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Test.AlgorithmsTests
{
    public class JpsTests
    {
        [Test]
        public void JpsTest()
        {
            new GridsTestController()
                .Run(q => new JpsDiagonal(q), true, true, MetricName.Euclidean);
        }
    }
}