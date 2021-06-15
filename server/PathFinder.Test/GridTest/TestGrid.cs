using System;
using System.Drawing;
using NUnit.Framework;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.GridTest
{
    [TestFixture]
    public class TestGrid
    {
        private Grid grid;
        
        [SetUp]
        public void SetUp()
        {
            grid = new Grid(new[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}});
        }
        
        [Test]
        [TestCase(0, 0, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 2, true)]
        [TestCase(2, 1, true)]
        [TestCase(0, -1, false)]
        [TestCase(-10, 0, false)]
        [TestCase(10, 0, false)]
        [TestCase(10, 10, false)]
        public void TestInBounds(int x, int y, bool expected)
        {
            Assert.AreEqual(expected, grid.InBounds(x, y));
        }

        [Test]
        [TestCase(0, 0, true)]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(0, 2, false)]
        [TestCase(1, 0, false)]
        public void TestIsPassable(int x, int y, bool expected)
        {
            grid = new Grid(new[,] {{10, 0, -1}, {-100, 1, 1}, {1, 1, 1}});
            
            Assert.AreEqual(expected, grid.IsPassable(x, y));
        }

        [Test]
        public void TestGetCost()
        {
            Assert.AreEqual(1, grid.GetCost(new Point(0, 0), new Point(1, 0)));
            Assert.AreEqual(Math.Sqrt(2), grid.GetCost(new Point(0, 0), new Point(1, 1)), 0.001);
        }
        
        [Test]
        public void TestGetCost2()
        {
            grid = new Grid(new[,] {{2, 2, 2}, {2, 2, 2}, {2, 2, 2}});
            
            Assert.AreEqual(2, grid.GetCost(new Point(0, 0), new Point(1, 0)));
            Assert.AreEqual(Math.Sqrt(2) * 2, grid.GetCost(new Point(0, 0), new Point(1, 1)), 0.001);
        }
        
        [Test]
        public void TestGetNeighborsWithIncorrectPoints()
        {
            CollectionAssert.AreEquivalent(Array.Empty<Point>(), 
                grid.GetNeighbors(new Point(-1, -1), false));
            
            CollectionAssert.AreEquivalent(Array.Empty<Point>(), 
                grid.GetNeighbors(new Point(100, 100), true));
        }

        [Test]
        public void TestGetNeighborsWithCenterPoint()
        {
            CollectionAssert.AreEquivalent(new Point[]{new (0, 1), new (1, 0), new(1, 2), new(2, 1)}, 
                grid.GetNeighbors(new Point(1, 1), false));
            
            CollectionAssert.AreEquivalent(new Point[]{
                    new (0, 1), new (1, 0), new(1, 2), new(2, 1),
                    new(0, 0), new(0, 2), new(2, 0), new(2, 2)}, 
                grid.GetNeighbors(new Point(1, 1), true));
        }

        [Test]
        public void TestGetNeighborsWithCornerPoint()
        {
            CollectionAssert.AreEquivalent(new Point[]{new(0, 1), new(1, 0)}, 
                grid.GetNeighbors(new Point(0, 0), false));
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 1), new(1, 0), new(1, 1)}, 
                grid.GetNeighbors(new Point(0, 0), true));
        }
        
        [Test]
        public void TestGetNeighborsWithWall()
        {
            grid = new Grid(new[,] {{1, -1, 1}, {1, 1, 1}, {1, 1, 1}});
            
            CollectionAssert.AreEquivalent(new Point[]{new(1, 0)}, 
                grid.GetNeighbors(new Point(0, 0), false));
            
            CollectionAssert.AreEquivalent(new Point[]{new(1, 0), new(1, 1)}, 
                grid.GetNeighbors(new Point(0, 0), true));
        }
        
        [Test]
        public void TestGetNeighborsWithNoWayBetweenWalls() // TODO fails. Need fix
        {
            // --8
            // -8x
            // ---
            grid = new Grid(new[,] {{1, 1, 1}, {1, -1, 1}, {1, 1, -1}});
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 2)}, 
                grid.GetNeighbors(new Point(1, 2), false));
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 2), new(0, 1)}, 
                grid.GetNeighbors(new Point(1, 2), true));
        }
    }
}