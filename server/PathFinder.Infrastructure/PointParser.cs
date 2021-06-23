using System.Drawing;

namespace PathFinder.Infrastructure
{
    public class PointParser
    {
        public static Point Parse(string msg)
        {
            var splited = msg.Split(',');
            return new Point(int.Parse(splited[0]), int.Parse(splited[1]));
        }
    }
}