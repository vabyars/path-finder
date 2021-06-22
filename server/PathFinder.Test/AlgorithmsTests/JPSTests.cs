﻿using System.Drawing;
using Moq;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.Realizations.JPS;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
using PathFinder.Infrastructure.PriorityQueue;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Test.AlgorithmsTests
{
    public class JpsTests
    {
        [Test]
        public void JpsTest()
        {
            var pq = new Mock<IPriorityQueueProvider<Point, IPriorityQueue<Point>>>();
            pq.Setup(x => x.Create())
                .Returns(() => new HeapPriorityQueue<Point>());
            new GridsTestController()
                .Run(new JpsDiagonal(new Mock<IRender>().Object, pq.Object), true, true, Metric.Euclidean);
        }
    }
}