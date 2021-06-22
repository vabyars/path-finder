using System.Drawing;

namespace PathFinder.Infrastructure
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color c) 
            => "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}