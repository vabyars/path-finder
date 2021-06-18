using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;
using PathFinder.Infrastructure.PriorityQueue;

namespace PathFinder.Domain.Models.Algorithms.Realizations.AStar
{
    public class AStarAlgorithm : AbstractAlgorithm
    {
        public override string Name => "A*";

        private IPriorityQueue<Point> queue;
        private readonly IPriorityQueueProvider<Point> queueProvider;

        private Dictionary<Point, Point> cameFrom = new();
        private Dictionary<Point, double> cost = new();

        private Point start;
        private Point goal;

        public AStarAlgorithm(IRender render, IPriorityQueueProvider<Point> queueProvider) : base(render)
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
        

        public override IEnumerable<IState> Run(IGrid grid, IParameters parameters)
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
                    yield return new ResultPathState()
                    {
                        Path = GetResultPath(),
                    };
                    yield break;
                }

                yield return new CurrentPointState
                {
                    PreparedPoint = current
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
                        yield return new CandidateToPrepareState
                        {
                            Candidate = neighbor
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