using System;
using System.Drawing;
using NUnit.Framework;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.GridTest
{
    [TestFixture]
    public class TestGrid
    {
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
            var grid = GetDefault3X3Grid();
            
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
            var grid = new Grid(new[,] {{10, 0, -1}, {-100, 1, 1}, {1, 1, 1}});
            
            Assert.AreEqual(expected, grid.IsPassable(x, y));
        }

        [Test]
        public void TestGetCost()
        {
            var grid = GetDefault3X3Grid();
            
            Assert.AreEqual(1, grid.GetCost(new Point(0, 0), new Point(1, 0)));
            Assert.AreEqual(Math.Sqrt(2), grid.GetCost(new Point(0, 0), new Point(1, 1)), 0.001);
        }
        
        [Test]
        public void TestGetCost2()
        {
            var grid = new Grid(new[,] {{2, 2, 2}, {2, 2, 2}, {2, 2, 2}});
            
            Assert.AreEqual(2, grid.GetCost(new Point(0, 0), new Point(1, 0)));
            Assert.AreEqual(Math.Sqrt(2) * 2, grid.GetCost(new Point(0, 0), new Point(1, 1)), 0.001);
        }
        
        [Test]
        public void TestGetNeighborsWithIncorrectPoints()
        {
            var grid = GetDefault3X3Grid();
            
            CollectionAssert.AreEquivalent(Array.Empty<Point>(), 
                grid.GetNeighbors(new Point(-1, -1), false));
            
            CollectionAssert.AreEquivalent(Array.Empty<Point>(), 
                grid.GetNeighbors(new Point(100, 100), true));
        }

        [Test]
        public void TestGetNeighborsWithCenterPoint()
        {
            var grid = GetDefault3X3Grid();
            
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
            var grid = GetDefault3X3Grid();
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 1), new(1, 0)}, 
                grid.GetNeighbors(new Point(0, 0), false));
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 1), new(1, 0), new(1, 1)}, 
                grid.GetNeighbors(new Point(0, 0), true));
        }
        
        [Test]
        public void TestGetNeighborsWithWall()
        {
            var grid = new Grid(new[,] {{1, -1, 1}, {1, 1, 1}, {1, 1, 1}});
            
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
            var grid = new Grid(new[,] {{1, 1, 1}, {1, -1, 1}, {1, 1, -1}});
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 2)}, 
                grid.GetNeighbors(new Point(1, 2), false));
            
            CollectionAssert.AreEquivalent(new Point[]{new(0, 2), new(0, 1)}, 
                grid.GetNeighbors(new Point(1, 2), true));
        }
        
        private Grid GetDefault3X3Grid() => new Grid(new[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}});
    }
}