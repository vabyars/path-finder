using System;
using System.Drawing;
using PathFinder.Domain;
using PathFinder.Domain.Models.Algorithms.AStar;

namespace TestLaunch
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(new [,] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, {1, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 1} });
            var parameters = new AStarParameters(new Point(0, 0), new Point(4, 3), true);
            var a = new AStarAlgorithm(new DictionaryPriorityQueue<Point>());
            foreach (var b in a.Run(grid, parameters))
            {
                Console.WriteLine(b.Point);
            }

            Console.WriteLine();
            foreach (var point in a.GetResultPath())
            {
                Console.WriteLine(point);
            }
        }
    }
}

// # # #
// # # #
// # # #