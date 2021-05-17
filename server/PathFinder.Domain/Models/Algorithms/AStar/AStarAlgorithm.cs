using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarAlgorithm : IAlgorithm<AStarState>
    {
        public string Name => "A*";

        private readonly IPriorityQueue<Point> queue;

        private readonly Dictionary<Point, Point> cameFrom = new();
        private readonly Dictionary<Point, double> cost = new();

        private Point start;
        private Point goal;

        public AStarAlgorithm(IPriorityQueue<Point> queue)
        {
            this.queue = queue;
        }

        public IEnumerable<AStarState> Run(IGrid grid, IParameters parameters)
        {
            start = parameters.Start;
            goal = parameters.End;
            queue.Add(start, 0);
            cameFrom.Add(start, start);
            cost.Add(start, 0);
            while (queue.Count != 0)
            {
                var (current, _) = queue.ExtractMin();
                if (current == goal)
                {
                    yield return new AStarState
                    {
                        Points = GetResultPath(),
                        Name = "result"
                    };
                    yield break;
                }

                yield return new AStarState
                {
                    Points = queue.ToList(),
                    Point = current,
                    Name = "текущая вершина"
                };
                var currentCost = cost[current];
                foreach (var neighbor in grid.GetNeighbors(current, parameters.AllowDiagonal))
                {
                    var newCost = currentCost + grid.GetCost(current, neighbor);
                    if (!cost.TryGetValue(neighbor, out var neighborCost) || newCost < neighborCost)
                    {
                        cost[neighbor] = newCost;
                        cameFrom[neighbor] = current;
                        queue.UpdateOrAdd(neighbor, newCost + parameters.Metric(neighbor, goal));
                        yield return new AStarState
                        {
                            Point = neighbor,
                            Name = "рассмотренная вершина"
                        };
                    }
                }
            }
        }

        private IEnumerable<Point> GetResultPath()
        {
            var path = new List<Point>();
            var current = goal;

            while (current != start)
            {
                if (!cameFrom.ContainsKey(current))
                {
                    return new List<Point>();
                }

                path.Add(current);
                current = cameFrom[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}