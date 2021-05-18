using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Test.AlgorithmsTests
{
    [TestFixture]
    public class AStartTests
    {
        [Test]
        public void AStarTest()
        {
            new GridsTestController()
                .Run(q => new AStarAlgorithm(q), true, false, MetricName.Euclidean);
        }
    }
}