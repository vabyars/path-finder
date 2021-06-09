using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.States.ResultPath;
using PathFinder.Test.AlgorithmsTests.TestGrids;

namespace PathFinder.Test.AlgorithmsTests
{
    public class GridsTestController
    {
        private readonly List<TestGrid> testGrids = new ()
        {
            new SimpleTestGrid(),
            new LargeSimpleGrid(),
            new SimpleMaze(),
            new SimpleMazeWithOneShortestHasPath(),
            new TestGridToCheckShortestHasPath(),
            new TestGridToCheckShortestPath2(),
        };
        
        public void Run(IAlgorithm algorithm,
            bool findsMinPath, bool worksOnlyWithDiagonal, MetricName metricName)
        {
            var metric = new MetricFactory().GetMetric(metricName);
            foreach (var testGrid in testGrids)
            {
                if (testGrid is IHasPath path && !worksOnlyWithDiagonal)
                {
                    var algorithmResultWithoutDiagonal = algorithm.Run(testGrid.Grid,
                        new Parameters(testGrid.Start, testGrid.Goal, false, metric));
                    
                    var lastState = algorithmResultWithoutDiagonal.Last() as ResultPathState;
                    Assert.NotNull(lastState, "last state of the algorithm must be \"ResultPathState\"");
                    AssertResultPath(lastState.Path, testGrid, findsMinPath, 
                        () => path.MinPathLength, () => path.MinPath, path.OnlyOneShortestPath);
                }
                
                if (testGrid is IHasDiagonalPath diagonalPath)
                {
                    var algorithmResultWithDiagonal = algorithm.Run(testGrid.Grid,
                        new Parameters(testGrid.Start, testGrid.Goal, true, metric));
                    var lastState = algorithmResultWithDiagonal.Last() as ResultPathState;
                    Assert.NotNull(lastState, "last state of the algorithm must be \"ResultPathState\"");
                    AssertResultPath(lastState.Path, testGrid, findsMinPath, 
                        () => diagonalPath.MinPathLengthWithDiagonal,
                        () => diagonalPath.MinPathWithDiagonal, diagonalPath.OnlyOneShortestDiagonalPath);
                }
            }   
        }

        private void AssertResultPath(IEnumerable<Point> resultPath, TestGrid testGrid, bool findsMinPath,
            Func<int> minPathLength, Func<IEnumerable<Point>> minPath, bool onlyOneShortestPath)
        {
            var mapName = $"Exception throw on {testGrid.GetType().Name}";
            var enumerable = resultPath as Point[] ?? resultPath.ToArray();
            if (findsMinPath)
            {
                Assert.AreEqual(minPathLength(), enumerable.Length, mapName);
                if (onlyOneShortestPath)
                    CollectionAssert.AreEqual(minPath(), enumerable, mapName);
            }
            Assert.AreEqual(testGrid.Start, enumerable.First(), mapName);
            Assert.AreEqual(testGrid.Goal, enumerable.Last(), mapName);
        }
    }
}