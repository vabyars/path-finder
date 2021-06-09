using System.Drawing;

namespace PathFinder.Domain.Models.Algorithms.Realizations.Lee
{
    public class LeeNode
    {
        public LeeNode(Point point, int costFromStart, LeeNode cameFrom)
        {
            Point = point;
            CostFromStart = costFromStart;
            CameFrom = cameFrom;
        }

        public Point Point { get; }
        public int CostFromStart { get;}
        public LeeNode CameFrom { get; }
    }
}