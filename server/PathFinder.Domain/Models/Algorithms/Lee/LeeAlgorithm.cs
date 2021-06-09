using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Renders;

namespace PathFinder.Domain.Models.Algorithms.Lee
{
    public class LeeAlgorithm : AbstractAlgorithm
    {
        public override string Name => "Lee";

        public LeeAlgorithm(IRender render) : base(render)
        {
            
        }
        public override IEnumerable<IState> Run(IGrid grid, IParameters parameters)
        {
            var queue = new Queue<LeeNode>();
            var visited = new HashSet<Point> {parameters.Start};
            queue.Enqueue(new LeeNode(parameters.Start, 0, null));
            
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current.Point == parameters.End)
                {
                    /*yield return new LeeState
                    {
                        ResultPath = GetResultPath(current).ToList(),
                        Cost = current.CostFromStart
                    };*/
                    yield break;
                }

                foreach (var neighbor in grid.GetNeighbors(current.Point, parameters.AllowDiagonal))
                {
                    if(visited.Contains(neighbor))
                        continue;
                    visited.Add(neighbor);
                    queue.Enqueue(new LeeNode(neighbor, current.CostFromStart+ 1, current));
                    /*yield return new LeeState
                    {
                        Point = neighbor,
                        Cost = current.CostFromStart + 1
                    };*/
                }
            }
        }

        private IEnumerable<Point> GetResultPath(LeeNode endNode)
        {
            var result = new List<Point>();
            while (endNode != null)
            {
                result.Add(endNode.Point);
                endNode = endNode.CameFrom;
            }

            result.Reverse();
            return result;
        }
    }
}