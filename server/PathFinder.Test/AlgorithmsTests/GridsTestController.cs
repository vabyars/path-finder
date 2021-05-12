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
        private readonly List<TestGrid> _testGrids = new ()
        {
            new SimpleTestGrid(),
            new LargeSimpleGrid(),
            new SimpleMaze(),
        };
        
        public void Run(Func<IPriorityQueue<Point>, IAlgorithm<State>> getInstance, bool findsMinPath, MetricName metric)
        {
            foreach (var testGrid in _testGrids)
            {
                var algorithmResultWithDiagonal = getInstance(new DictionaryPriorityQueue<Point>()).Run(testGrid.Grid,
                    new Parameters(testGrid.Start, testGrid.Goal, true, new MetricFactory().GetMetric(metric)));
                
                var algorithmResultWithoutDiagonal = getInstance(new DictionaryPriorityQueue<Point>()).Run(testGrid.Grid,
                    new Parameters(testGrid.Start, testGrid.Goal, false, new MetricFactory().GetMetric(metric)));

                if (testGrid is IPath path)
                {
                    AssertResultPath(algorithmResultWithoutDiagonal.Last().Points, testGrid, findsMinPath, 
                        () => path.MinPathLength, () => path.MinPath, path.OnlyOneShortestPath);
                }
                
                if (testGrid is IDiagonalPath diagonalPath)
                {
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