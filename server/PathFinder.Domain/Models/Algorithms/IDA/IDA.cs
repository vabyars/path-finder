using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Infrastructure;

namespace PathFinder.Domain.Models.Algorithms.IDA
{
    public class IDA : IAlgorithm<IDAState>
    {
        private readonly Dictionary<Point, Point> parentMap = new();
        private Func<Point, Point, double> metric;
        private IParameters parameters;
        private IGrid grid; 
        private Point start;
        private Point goal;

        public string Name => "IDA*";

        public IEnumerable<IDAState> Run(IGrid grid, IParameters parameters)
        {
            start = parameters.Start;
            goal = parameters.End;
            metric = parameters.Metric;
            this.parameters = parameters;
            this.grid = grid;
            var path = GetPath().ToList();
            path.Reverse();
            yield return new IDAState
            {
                Points = path,
                Name = "result"
            };
        }

        private IEnumerable<Point> GetPath()
        {
            var currentFBound = metric(start, goal);
            var path = new Stack<Point>();
            path.Push(start);
            do
            {
                var smallestFBound = Recursive(path, 0, currentFBound);
                if (smallestFBound == 0.0)
                    return Backtrace(path.Peek());
                currentFBound = smallestFBound;
            } while (Math.Abs(currentFBound - double.MaxValue) > double.Epsilon);

            return null;
        }

        private double Recursive(Stack<Point> path, double distanceToStart, double bound)
        {
            var node = path.Peek();
            var distanceToStartAndEstimateToEnd = distanceToStart + metric(node, goal);
            if (distanceToStartAndEstimateToEnd > bound)
                return distanceToStartAndEstimateToEnd;
            if (node == goal)
                return 0.0;
            var min = double.MaxValue;
            var neighbors = grid.GetNeighbors(node, parameters.AllowDiagonal);
            var queue = new HeapPriorityQueue<Point>();
            
            foreach (var neighbor in neighbors)
            {
                var priority = distanceToStart + grid.GetCost(neighbor, node) + metric(neighbor, goal);
                queue.Add(neighbor, priority);
            }

            foreach (var neighbor in queue)
            {
                if (path.Contains(neighbor)) continue;
                path.Push(neighbor);
                parentMap[neighbor] = node;
                var t = Recursive(path, distanceToStart + grid.GetCost(neighbor, node), bound);
                if (t == 0.0)
                    return 0.0;
                min = Math.Min(min, t);
                path.Pop();
            }

            return min;
        }

        private IEnumerable<Point> Backtrace(Point endPathNode) 
        {
            var path = new List<Point> { endPathNode };
            while (parentMap.ContainsKey(endPathNode)) 
            {
                path.Add(parentMap[endPathNode]);
                endPathNode = parentMap[endPathNode];
            }

            return path;
        }
    }
}