using System.Drawing;

namespace PathFinder.Domain.Models.GridFolder
{
    public class GridWithStartAndEnd
    {
        public int[,] Maze { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
    }
}