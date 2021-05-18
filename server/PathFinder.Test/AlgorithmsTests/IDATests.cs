using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Test.AlgorithmsTests
{
    [TestFixture]
    public class IDATests
    {
        [Test]
        public void IDATest()
        {
            new GridsTestController()
                .Run(q => new IDA(), true, MetricName.Euclidean);
        }
    }
}