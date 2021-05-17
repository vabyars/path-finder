using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Domain.Models.Algorithms.JPS
{
    public class JpsDiagonal : IAlgorithm<JumpPointSearchState>
    {
        private readonly Dictionary<Point, double>
            distanceToStart = new(); // distance to start (parent's g + distance from parent)

        private readonly Dictionary<Point, double> distanceToStartAndEstimateToEnd = new();
        private readonly Dictionary<Point, double> estimateDistanceToEnd = new();
        private readonly Dictionary<Point, Point> parentMap = new();

        private readonly List<Point> jumpPoints = new();
        
        private Point goal;
        private HashSet<Point> goalNeighbours = new();
        private Point start;
        private Func<Point, Point, double> metric;
        private IPriorityQueue<Point> priorityQueue;
        public string Name => "JPS";

        public JpsDiagonal(IPriorityQueue<Point> queue)
        {
            priorityQueue = queue;
        }
        
        public IEnumerable<JumpPointSearchState> Run(IGrid grid, IParameters parameters)
        {
            start = parameters.Start;
            goal = parameters.End;
            metric = parameters.Metric;
            goalNeighbours = grid.GetNeighbors(goal, parameters.AllowDiagonal).ToHashSet();
            var path = FindPath(grid).ToList();
            foreach (var jumpPoint in jumpPoints)
                yield return new JumpPointSearchState
                {
                    Point = jumpPoint,
                    Name = "Jump point"
                };
            
            yield return new JumpPointSearchState
            {
                Points = path,
                Name = "result"
            };
        }

        private IEnumerable<Point> FindPath(IGrid grid)
        {
            var closed = new HashSet<Point>();

            if (grid.IsPassable(goal))
                goalNeighbours.Add(goal);

            if (goalNeighbours.Count == 0)
                return null;
            priorityQueue.Add(start, 0);

            while (priorityQueue.Count > 0)
            {
                var (current, _) = priorityQueue.ExtractMin();
                closed.Add(current);

                if (goalNeighbours.Contains(current))
                    return Backtrace(current);
                
                IdentifySuccessors(current, priorityQueue, closed, grid);
            }

            return null;
        }

        private void IdentifySuccessors(Point point, IPriorityQueue<Point> open, IReadOnlySet<Point> closed, IGrid grid)
        {
            foreach (var neighbor in FindNeighbors(point, grid))
            {
                var rawJumpPoint = Jump(neighbor, point, grid);
                
                if (rawJumpPoint == null || closed.Contains(rawJumpPoint.Value))
                    continue;
                var jumpPoint = rawJumpPoint.Value;
                
                var distance = distanceToStart.ContainsKey(point) ? distanceToStart[point] : 0 + metric(jumpPoint, point);

                if (!open.TryGetValue(jumpPoint, out _) || distance < distanceToStart[jumpPoint])
                {
                    distanceToStart[jumpPoint] = distance;
                    estimateDistanceToEnd[jumpPoint] = metric(jumpPoint, goal);
                    distanceToStartAndEstimateToEnd[jumpPoint] = 
                        distanceToStart[jumpPoint] + estimateDistanceToEnd[jumpPoint];
                    Console.WriteLine("jumpPoint: " + jumpPoint + " f: " + distanceToStartAndEstimateToEnd[jumpPoint]);
                    parentMap[jumpPoint] = point;
                    jumpPoints.Add(jumpPoint);
                    if (!open.TryGetValue(jumpPoint, out _))
                    {
                        open.Add(jumpPoint, distanceToStartAndEstimateToEnd[jumpPoint]);
                    }
                }
            }
        }

        private IEnumerable<Point> FindNeighbors(Point point, IGrid grid)
        {
            var (x, y) = (point.X, point.Y);
            if (parentMap.ContainsKey(point))
            {
                var parent = parentMap[point];
                var dx = GetNormalizedDirection(x, parent.X);
                var dy = GetNormalizedDirection(y, parent.Y);

                if (dx != 0 && dy != 0) {
                    if (grid.IsPassable(x, y + dy)) 
                        yield return new Point(x, y + dy);
                    
                    if (grid.IsPassable(x + dx, y)) 
                        yield return new Point(x + dx, y);
                    
                    if (grid.IsPassable(x, y + dy) || grid.IsPassable(x + dx, y)) 
                        yield return new Point(x + dx, y + dy);
                    
                    if (!grid.IsPassable(x - dx, y) && grid.IsPassable(x, y + dy)) 
                        yield return new Point(x - dx, y + dy);
                    
                    if (!grid.IsPassable(x, y - dy) && grid.IsPassable(x + dx, y)) 
                        yield return new Point(x + dx, y - dy);
                    
                }
                else {
                    if (dx == 0)
                    {
                        if (!grid.IsPassable(x, y + dy)) 
                            yield break;
                        yield return new Point(x, y + dy);
                        if (!grid.IsPassable(x + 1, y)) 
                            yield return new Point(x + 1, y + dy);
                        if (!grid.IsPassable(x - 1, y)) 
                            yield return new Point(x - 1, y + dy);
                    }
                    else
                    {
                        if (!grid.IsPassable(x + dx, y)) 
                            yield break;
                        yield return new Point(x + dx, y);
                        if (!grid.IsPassable(x, y + 1)) 
                            yield return new Point(x + dx, y + 1);
                        if (!grid.IsPassable(x, y - 1))
                            yield return new Point(x + dx, y - 1);
                    }
                }
            }
            else {
                foreach (var neighbor in GetNeighbors(point, grid))
                    yield return neighbor;
            }
        }
        
        private IEnumerable<Point> GetNeighbors(Point point, IGrid grid)
        {
            var (x, y) = (point.X, point.Y);
            var (walkedUp, walkedRight, walkedDown, walkedLeft) = (false, false, false, false);

            if (grid.IsPassable(x, y - 1)) 
            {
                yield return new Point(x, y - 1);
                walkedUp = true;
            }

            if (grid.IsPassable(x + 1, y)) 
            {
                yield return new Point(x + 1, y);
                walkedRight = true;
            }

            if (grid.IsPassable(x, y + 1)) 
            {
                yield return new Point(x, y + 1);
                walkedDown = true;
            }

            if (grid.IsPassable(x - 1, y)) 
            {
                yield return new Point(x - 1, y);
                walkedLeft = true;
            }

            var walked = new[]
            {
                walkedLeft || walkedUp, walkedUp || walkedRight, 
                walkedRight || walkedDown, walkedDown || walkedLeft
            };
            var walkedPoints = new Point[]
            {
                new(x - 1, y - 1), new(x + 1, y - 1), 
                new(x + 1, y + 1), new(x - 1, y + 1)
            };
            foreach (var (isWalked, direction) in walked.Zip(walkedPoints))
            {
                if (isWalked && grid.IsPassable(direction))
                    yield return direction;
            }
        }

        private Point? Jump(Point point, Point parentPoint, IGrid grid)
        {
            if (!grid.IsPassable(point))
                return null;
            if (goalNeighbours.Contains(point))
                return point;
            var (x, y) = (point.X, point.Y);
            var (dx, dy) = GetDelta(point, parentPoint);
            if (dx != 0 && dy != 0)
            {
                if (IsWalkableDiagonal(grid, x, y, dx, dy) || HasHorizontalOrVerticalJumpPoints(grid, point, dx, dy))
                    return point;
            }
            else
            {
                if (dx != 0) 
                {
                    if (IsWalkableHorizontal(grid, x, y, dx)) 
                        return point;
                }
                else 
                {
                    if (IsWalkableVertical(grid, x, y, dy)) 
                        return point;
                }
            }

            if (grid.IsPassable(x + dx, y) || grid.IsPassable(x, y + dy))
                return Jump(new Point(x + dx, y + dy), point, grid);
            return null;
        }

        private IEnumerable<Point> Backtrace(Point point)
        {
            var path = new LinkedList<Point>();
            path.AddFirst(point);
            while (parentMap.ContainsKey(point))
            {
                var (previousX, previousY) = GetCoordinatesFromPoint(parentMap[point]);
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

                point = parentMap[point];
            }

            return path;
        }

        private (int, int) GetCoordinatesFromPoint(Point point) => (point.X, point.Y);

        private int GetNormalizedDirection(int current, int previous) =>
            (current - previous) / Math.Max(Math.Abs(current - previous), 1);

        private (int, int) GetDelta(Point to, Point from) => (to.X - from.X, to.Y - from.Y);

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
    }
}