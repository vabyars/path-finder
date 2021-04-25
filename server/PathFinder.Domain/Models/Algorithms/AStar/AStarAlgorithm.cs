using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarAlgorithm : IAlgorithm<AStarState, AStarParameters>
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
        
        public IEnumerable<AStarState> Run(IGrid grid, AStarParameters parameters)
        {
            _start = parameters.Start;
            _goal = parameters.End;
            _queue.Add(_start, 0);
            _cameFrom.Add(_start, _start);
            _cost.Add(_start, 0);

            while (_queue.Count != 0)
            {
                var (current, priority) = _queue.ExtractMin();
                if (current == _goal)
                {
                    yield return new AStarState(current); //TODO fix
                    yield break;
                }
                var currentCost = _cost[current];
                foreach (var point in grid.GetNeighbors(current, parameters.AllowDiagonal))
                {
                    var newCost = currentCost + grid[point]; //TODO add diagonal modifier
                    if (!_cost.TryGetValue(point, out var neighborCost) || newCost < neighborCost)
                    {
                        _cost[point] = newCost;
                        _cameFrom[point] = current;
                        _queue.UpdateOrAdd(point, newCost + GetHeuristicPathLength(point, _goal));
                        yield return new AStarState(point);
                    }
                }
            }
            //текущая вершина
            //список оставшихся вершин
            
            //статистика алгоритма в самом конце
        }

        private static double GetHeuristicPathLength(Point from, Point to)
            => Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);

        public IEnumerable<Point> GetResultPath() {

            var path = new List<Point>();
            var current = _goal; 

            while (!(current == _start)) {
                if (!_cameFrom.ContainsKey(current))
                {
                    Console.WriteLine(current);
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