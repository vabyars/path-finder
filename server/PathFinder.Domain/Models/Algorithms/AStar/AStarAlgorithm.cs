using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Interfaces;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarAlgorithm : IAlgorithm<AStarState>
    {
        public string Name => "A*";

        private readonly IPriorityQueue<Point> _queue;

        private readonly Dictionary<Point, Point> _cameFrom = new();
        private readonly Dictionary<Point, double> _cost = new();

        private Point _start;
        private Point _goal;

        public AStarAlgorithm(IPriorityQueue<Point> queue)
        {
            _queue = queue;
        }

        public IEnumerable<AStarState> Run(IGrid grid, IParameters parameters)
        {
            _start = parameters.Start;
            _goal = parameters.End;
            _queue.Add(_start, 0);
            _cameFrom.Add(_start, _start);
            _cost.Add(_start, 0);
            while (_queue.Count != 0)
            {
                var (current, _) = _queue.ExtractMin();
                if (current == _goal)
                {
                    yield return new AStarState
                    {
                        Points = GetResultPath(),
                        Name = "result"
                    };
                    yield break;
                }

                /*yield return new AStarState
                {
                    Points = _queue.GetAllItems(),
                    Point = current,
                    Name = "текущая вершина"
                };*/
                var currentCost = _cost[current];
                foreach (var neighbor in grid.GetNeighbors(current, parameters.AllowDiagonal))
                {
                    var newCost = currentCost + grid.GetCost(current, neighbor);
                    if (!_cost.TryGetValue(neighbor, out var neighborCost) || newCost < neighborCost)
                    {
                        _cost[neighbor] = newCost;
                        _cameFrom[neighbor] = current;
                        _queue.UpdateOrAdd(neighbor, newCost + parameters.Metric(neighbor, _goal));
                        /*yield return new AStarState
                        {
                            Point = neighbor,
                            Name = "рассмотренная вершина"
                        };*/
                    }
                }
            }
        }

        private IEnumerable<Point> GetResultPath()
        {
            var path = new List<Point>();
            var current = _goal;

            while (current != _start)
            {
                if (!_cameFrom.ContainsKey(current))
                {
                    return new List<Point>();
                }

                path.Add(current);
                current = _cameFrom[current];
            }

            path.Add(_start);
            path.Reverse();
            return path;
        }
    }
}