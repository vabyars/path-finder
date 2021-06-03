using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Interfaces;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.AStar
{
    public class AStarAlgorithm : IAlgorithm<AStarState>
    {
        public string Name => "A*";

        private IPriorityQueue<Point> queue;
        private readonly IPriorityQueueProvider<Point> queueProvider;

        private Dictionary<Point, Point> cameFrom = new();
        private Dictionary<Point, double> cost = new();

        private Point start;
        private Point goal;

        public AStarAlgorithm(IPriorityQueueProvider<Point> queueProvider)
        {
            this.queueProvider = queueProvider;
        }

        private void Init(IParameters parameters)
        {
            start = parameters.Start;
            goal = parameters.End;
            queue = queueProvider.Create();
            cameFrom = new Dictionary<Point, Point>();
            cost = new Dictionary<Point, double>();
        }
        

        public IEnumerable<AStarState> Run(IGrid grid, IParameters parameters)
        {
            Init(parameters);
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
                        ResultPath = GetResultPath(),
                        Name = "result"
                    };
                    yield break;
                }

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