using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using PathFinder.Domain;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Test.AlgorithmsTests
{
    [TestFixture]
    public class AStartTests
    {
        private IPriorityQueue<Point> _queue;
        private AStarAlgorithm _algorithm;

        [SetUp]
        public void Init()
        {
            _queue = new DictionaryPriorityQueue<Point>();
            _algorithm = new AStarAlgorithm(_queue);
        }

        [Test]
        public void SimpleTest()
        {
            new GridsController()
                .Run(new AStarAlgorithm(_queue), true, MetricName.Euclidean);
            /*var grid = new Grid(new[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}});
            var parameters = new Parameters(new Point(0, 0), new Point(2, 2), false);

            var result = _algorithm.Run(grid, parameters);
            var expectedPath = new List<Point> {new(0, 0), new(1, 0), new(2, 0), new(2, 1), new(2, 2)};
            var aStarStates = result as AStarState[] ?? result.ToArray();
            //Assert.NotNull(aStarStates.Last().Points);
            //Assert.AreEqual(5, aStarStates.Last().Points.Count());
            //CollectionAssert.AreEqual(expectedPath, aStarStates.Last().Points);*/
        }
    }
}