﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Domain.Models.Algorithms.Realizations.IDA
{
    public class IDA : AbstractAlgorithm
    {
        private Dictionary<Point, Point> parentMap = new();
        private Metric metric;
        private IParameters parameters;
        private IGrid grid; 
        private Point start;
        private Point goal;

        public override string Name => "IDA*";

        public IDA(IRender render) : base(render)
        {
            
        }

        public override IEnumerable<IState> Run(IGrid grid, IParameters parameters)
        {
            start = parameters.Start;
            goal = parameters.End;
            metric = parameters.Metric;
            parentMap = new Dictionary<Point, Point>();
            this.parameters = parameters;
            this.grid = grid;
            var path = GetPath().ToList();
            path.Reverse();
            yield break;
            /*yield return new IDAState
            {
                ResultPath = path,
                Name = "result"
            };*/
        }

        private IEnumerable<Point> GetPath()
        {
            var bound = metric.Call(start, goal);
            var path = new Stack<Point>();
            path.Push(start);
            do
            {
                var distance = Recursive(path, 0, bound);
                if (distance == 0.0)
                    return Backtrace(path.Peek());
                bound = distance;
            } while (Math.Abs(bound - double.MaxValue) > double.Epsilon);

            return null;
        }

        private double Recursive(Stack<Point> path, double distance, double bound)
        {
            var node = path.Peek();
            var estimate = distance + metric.Call(node, goal);
            if (estimate > bound)
                return estimate;
            if (node == goal)
                return 0.0;
            var min = double.MaxValue;
            var neighbors = grid.GetNeighbors(node, parameters.AllowDiagonal);
            var queue = new HeapPriorityQueue<Point>();
            
            foreach (var neighbor in neighbors)
            {
                var priority = distance + grid.GetCost(neighbor, node) + metric.Call(neighbor, goal);
                queue.Add(neighbor, priority);
            }

            foreach (var neighbor in queue)
            {
                if (path.Contains(neighbor)) continue;
                path.Push(neighbor);
                parentMap[neighbor] = node;
                var t = Recursive(path, distance + grid.GetCost(neighbor, node), bound);
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