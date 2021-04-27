using System.Drawing;

namespace PathFinder.Infrastructure
{
    public class PointParser
    {
        public static Point Parse(string msg)
        {
            var splitted = msg.Split(',');
            return new Point(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }
    }
}