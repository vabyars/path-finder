using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.Lee;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Test.AlgorithmsTests
{
    public class LeeTests
    {
        [Test]
        public void LeeTest()
        {
            new GridsTestController()
                .Run(new LeeAlgorithm(), true, false, MetricName.Euclidean);
        }
    }
}