using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.JPS
{
    public class JpsDiagonal : IAlgorithm<JumpPointSearchState>
    {
        private readonly IPriorityQueue<Point> _openQueue;

        private readonly Dictionary<Point, double>
            _distanceToStart = new(); // distance to start (parent's g + distance from parent)

        private readonly Dictionary<Point, double> _distanceToStartAndEstimateToEnd = new();
        private readonly Dictionary<Point, double> _estimateDistanceToEnd = new();
        private readonly Dictionary<Point, Point> _parentMap = new();
        
        private Point _goal;
        private HashSet<Point> _goalNeighbours = new();
        private Point _start;
        private Func<Point, Point, double> _metric;
        public string Name => "JPS";

        public JpsDiagonal(IPriorityQueue<Point> openQueue)
        {
            _openQueue = openQueue;
        }
        
        public IEnumerable<JumpPointSearchState> Run(IGrid grid, IParameters parameters)
        {
            _start = parameters.Start;
            _goal = parameters.End;
            _metric = parameters.Metric;
            _goalNeighbours = grid.GetNeighbors(_goal, false).ToHashSet();
            yield return new JumpPointSearchState
            {
                Points = FindPathSync(grid).ToList()
            };
            /*foreach (var point in FindPathSync(grid))
                yield return new JumpPointSearchState(point);*/
        }

        private IEnumerable<Point> FindPathSync(IGrid grid)
        {
            var closed = new HashSet<Point>();

            if (grid.IsPassable(_goal))
                _goalNeighbours.Add(_goal);

            if (_goalNeighbours.Count == 0)
                return null;
            Console.WriteLine(_start);
            _openQueue.Add(_start, 0);

            while (_openQueue.Count > 0)
            {
                var (current, _) = _openQueue.ExtractMin();
                closed.Add(current);

                if (_goalNeighbours.Contains(current))
                    return Backtrace(current);

                // add all possible next steps from the current point
                IdentifySuccessors(current, _openQueue, closed, grid);
            }

            return null;
        }

        private void IdentifySuccessors(Point point, IPriorityQueue<Point> open, IReadOnlySet<Point> closed, IGrid grid)
        {
            // get all neighbors to the current point
            foreach (var neighbor in FindNeighbors(point, grid))
            {
                // jump in the direction of our neighbor
                var rawJumpPoint = Jump(neighbor, point, grid);

                // don't add a point we have already gotten to quicker
                if (rawJumpPoint == null || closed.Contains(rawJumpPoint.Value))
                    continue;
                var jumpPoint = rawJumpPoint.Value;
                // determine the jumpPoint's distance from the start along the current path
                var d = GetEuclideanLength(jumpPoint, point);
                var ng = _distanceToStart.ContainsKey(point) ? _distanceToStart[point] : 0 + d;

                // if the point has already been opened and this is a shorter path, update it
                // if it hasn't been opened, mark as open and update it
                if (!open.TryGetValue(jumpPoint, out _) || ng < _distanceToStart[jumpPoint])
                {
                    _distanceToStart.Add(jumpPoint, ng);
                    _estimateDistanceToEnd.Add(jumpPoint, _metric(jumpPoint, _goal));
                    _distanceToStartAndEstimateToEnd.Add(jumpPoint,
                        _distanceToStart[jumpPoint] + _estimateDistanceToEnd[jumpPoint]);
                    Console.WriteLine("jumpPoint: " + jumpPoint + " f: " + _distanceToStartAndEstimateToEnd[jumpPoint]);
                    _parentMap.Add(jumpPoint, point);

                    if (!open.TryGetValue(jumpPoint, out _)) 
                        open.Add(jumpPoint, 0);
                }
            }
        }

        private IEnumerable<Point> FindNeighbors(Point point, IGrid grid)
        {
            var (x, y) = GetCoordinatesFromPoint(point);
            if (_parentMap.ContainsKey(point))
            {
                var parentPoint = _parentMap[point];
                var (parentX, parentY) = GetCoordinatesFromPoint(parentPoint);
                var dx = GetNormalizedDirection(x, parentX);
                var dy = GetNormalizedDirection(y, parentY);
                if (dx != 0 && dy != 0)
                {
                    if (grid.IsPassable(x, y + dy))
                        yield return new Point(x, y + dy);

                    if (grid.IsPassable(x + dx, y))
                        yield return new Point(x + dx, y);

                    if (grid.IsPassable(x + dx, y + dy))
                        yield return new Point(x + dx, y + dy);

                    if (!grid.IsPassable(x - dx, y))
                        yield return new Point(x - dx, y + dy);

                    if (!grid.IsPassable(x, y - dy))
                        yield return new Point(x + dx, y - dy);
                }
                else // search horizontally/vertically
                {
                    if (grid.IsPassable(x + dx, y + dy))
                        yield return new Point(x + dx, y + dy);
                    
                    if (dx == 0)
                    {
                        dx = 1;
                        if (!grid.IsPassable(x + dx, y))
                            yield return new Point(x + dx, y + dy);
                        if (!grid.IsPassable(x - dx, y))
                            yield return new Point(x - dx, y + dy);
                    }
                    else
                    {
                        dy = 1;
                        if (!grid.IsPassable(x, y + dy))
                            yield return new Point(x + dx, y + dy);
                        if (!grid.IsPassable(x, y - dy))
                            yield return new Point(x + dx, y - dy);
                    }
                }
            }
            else
            {
                foreach (var neighbor in grid.GetNeighbors(point, true))
                {
                    yield return neighbor;
                }
            }
        }

        private Point? Jump(Point point, Point parentPoint, IGrid grid)
        {
            if (!grid.IsPassable(point))
                return null;
            if (_goalNeighbours.Contains(point))
                return point;
            var (dx, dy) = GetDelta(parentPoint, point);
            if (dx != 0 && dy != 0)
            {
                if (IsWalkableDiagonal(grid, point.X, point.Y, dx, dy))
                    return point;
                if (HasHorizontalOrVerticalJumpPoints(grid, point, dx, dy))
                    return point;
            }
            else
            {
                if (dx != 0)
                {
                    if (IsWalkableHorizontal(grid, point.X, point.Y, dx))
                        return point;
                }
                else
                {
                    if (IsWalkableVertical(grid, point.X, point.Y, dy))
                        return point;
                }
            }

            return Jump(new Point(point.X + dx, point.Y + dy), point, grid);
        }

        private IEnumerable<Point> Backtrace(Point point)
        {
            var path = new LinkedList<Point>();
            path.AddFirst(point);
            while (_parentMap.ContainsKey(point))
            {
                var (previousX, previousY) = GetCoordinatesFromPoint(_parentMap[point]);
                var (currentX, currentY) = GetCoordinatesFromPoint(point);
                var steps = Math.Max(Math.Abs(previousX - currentX), Math.Abs(previousY - currentY));
                var dx = previousX.CompareTo(currentX);
                var dy = previousY.CompareTo(currentY);
                var temp = point;
                for (var i = 0; i < steps; i++)
                {
                    temp = new Point(temp.X + dx, temp.Y + dy);
                    path.AddFirst(temp);
                }

                point = _parentMap[point];
            }

            return path;
        }

        private (int, int) GetCoordinatesFromPoint(Point point) => (point.X, point.Y);

        private int GetNormalizedDirection(int current, int previous) =>
            (current - previous) / Math.Max(Math.Abs(current - previous), 1);

        private (int, int) GetDelta(Point from, Point to) => (to.X - from.X, to.Y - from.Y);

        private bool IsWalkableDiagonal(IGrid grid, int x, int y, int dx, int dy) =>
            grid.IsPassable(x - dx, y + dy) && !grid.IsPassable(x - dx, y) ||
            grid.IsPassable(x + dx, y - dy) && !grid.IsPassable(x, y - dy);

        private bool IsWalkableHorizontal(IGrid grid, int x, int y, int dx) =>
            grid.IsPassable(x + dx, y + 1) && !grid.IsPassable(x, y + 1) ||
            grid.IsPassable(x + dx, y - 1) && !grid.IsPassable(x, y - 1);

        private bool IsWalkableVertical(IGrid grid, int x, int y, int dy) =>
            grid.IsPassable(x + 1, y + dy) && !grid.IsPassable(x + 1, y) ||
            grid.IsPassable(x - 1, y + dy) && !grid.IsPassable(x - 1, y);

        private bool HasHorizontalOrVerticalJumpPoints(IGrid grid, Point point, int dx, int dy) =>
            Jump(new Point(point.X + dx, point.Y), point, grid).HasValue ||
            Jump(new Point(point.X, point.Y + dy), point, grid).HasValue;

        private static double GetEuclideanLength(Point from, Point to)
            => Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
    }
}