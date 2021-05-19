using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.States;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;
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
            new SimpleMazeWithOneShortestPath(),
            new TestGridToCheckShortestPath(),
            new TestGridToCheckShortestPath2(),
            new TestGridWithZeroCostCells()
        };
        
        public void Run(Func<IPriorityQueue<Point>, IAlgorithm<State>> getInstance,
            bool findsMinPath, bool worksOnlyWithDiagonal, MetricName metricName)
        {
            var metric = new MetricFactory().GetMetric(metricName);
            foreach (var testGrid in testGrids)
            {
                if (testGrid is IPath path && !worksOnlyWithDiagonal)
                {
                    var algorithmResultWithoutDiagonal = getInstance(new HeapPriorityQueue<Point>()).Run(testGrid.Grid,
                        new Parameters(testGrid.Start, testGrid.Goal, false, metric));
                    
                    AssertResultPath(algorithmResultWithoutDiagonal.Last().Points, testGrid, findsMinPath, 
                        () => path.MinPathLength, () => path.MinPath, path.OnlyOneShortestPath);
                }
                
                if (testGrid is IDiagonalPath diagonalPath)
                {
                    var algorithmResultWithDiagonal = getInstance(new HeapPriorityQueue<Point>()).Run(testGrid.Grid,
                        new Parameters(testGrid.Start, testGrid.Goal, true, metric));
                    
                    AssertResultPath(algorithmResultWithDiagonal.Last().Points, testGrid, findsMinPath, 
                        () => diagonalPath.MinPathLengthWithDiagonal,
                        () => diagonalPath.MinPathWithDiagonal, diagonalPath.OnlyOneShortestDiagonalPath);
                }
            }   
        }

        private void AssertResultPath(IEnumerable<Point> resultPath, TestGrid testGrid, bool findsMinPath,
            Func<int> minPathLength, Func<IEnumerable<Point>> minPath, bool onlyOneShortestPath)
        {
            if (findsMinPath)
            {
                Assert.AreEqual(minPathLength(), resultPath.Count());
                if (onlyOneShortestPath)
                    CollectionAssert.AreEqual(minPath(), resultPath);
            }
            Assert.AreEqual(testGrid.Start, resultPath.First());
            Assert.AreEqual(testGrid.Goal, resultPath.Last());
        }
    }
}