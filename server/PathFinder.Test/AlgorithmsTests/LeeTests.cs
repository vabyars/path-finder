﻿using Moq;
using NUnit.Framework;
using PathFinder.Domain.Models.Algorithms.Realizations.Lee;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;

namespace PathFinder.Test.AlgorithmsTests
{
    public class LeeTests
    {
        [Test]
        public void LeeTest()
        {
            new GridsTestController()
                .TestOnUsualGrids(new LeeAlgorithm(new Mock<IRender>().Object), true, false, Metric.Euclidean);
        }
    }
}