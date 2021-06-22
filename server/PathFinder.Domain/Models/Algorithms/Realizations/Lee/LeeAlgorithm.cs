using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;

namespace PathFinder.Domain.Models.Algorithms.Realizations.Lee
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
                    yield return new ResultPathState
                    {
                        Path = GetResultPath(current)
                    };
                    yield break;
                }
                
                yield return new CurrentPointState
                {
                    PreparedPoint = current.Point
                };

                foreach (var neighbor in grid.GetNeighbors(current.Point, parameters.AllowDiagonal))
                {
                    if(visited.Contains(neighbor))
                        continue;
                    visited.Add(neighbor);
                    queue.Enqueue(new LeeNode(neighbor, current.CostFromStart+ 1, current));
                    yield return new CandidateToPrepareState
                    {
                        Candidate = neighbor
                    };
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