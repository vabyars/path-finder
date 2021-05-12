using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.States;
using PathFinder.Test.AlgorithmsTests.TestGrids;

namespace PathFinder.Test.AlgorithmsTests
{
    public class GridsController
    {
        private List<TestGrid> _testGrids = new ()
        {
            new TestGrid(),
        };
        
        public void Run(IAlgorithm<State> algorithm, bool findsMinPath, MetricName metric)
        {
            foreach (var testGrid in _testGrids)
            {
                var algorithmResultWithDiagonal = algorithm.Run(testGrid.Grid,
                    new Parameters(testGrid.Start, testGrid.Goal, true, new MetricFactory().GetMetric(metric)));
                
                var algorithmResultWithoutDiagonal = algorithm.Run(testGrid.Grid,
                    new Parameters(testGrid.Start, testGrid.Goal, false, new MetricFactory().GetMetric(metric)));

                if (testGrid.OnlyOneShortestPath && findsMinPath)
                {
                    var resPath = algorithmResultWithoutDiagonal.Last().Points;
                    Assert.AreEqual(testGrid.MinPathLength, resPath.Count());
                    CollectionAssert.AreEqual(testGrid.MinPath, resPath);
                }
                
                if (testGrid.OnlyOneShortestDiagonalPath && findsMinPath)
                {
                    var resPath = algorithmResultWithDiagonal.Last().Points;
                    Assert.AreEqual(testGrid.MinPathLengthWithDiagonal, resPath.Count());
                    CollectionAssert.AreEqual(testGrid.MinPathWithDiagonal, resPath);
                }
            }   
        }
    }
}