using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Test.AlgorithmsTests
{
    public class IDATests
    {
        [Test]
        public void IDATest()
        {
            new GridsTestController()
                .Run(q => new IDA(), true, true, MetricName.Euclidean);
        }
    }
}