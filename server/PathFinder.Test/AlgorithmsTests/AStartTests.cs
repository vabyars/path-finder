using System.Drawing;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
using PathFinder.Infrastructure;

namespace PathFinder.Test.AlgorithmsTests
{
    [TestFixture]
    public class AStartTests
    {
        [Test]
        public void AStarTest()
        {
            new GridsTestController() //TODO пиздец хуйня, fix!
                .Run(new AStarAlgorithm(new AStarRenderNew(),new PriorityQueueProvider<Point>()), true, false, MetricName.Euclidean);
        }
    }
}