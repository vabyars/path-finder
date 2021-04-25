using System.Drawing;

namespace PathFinder.Domain.Models.States
{
    public class CurrentPointState : State
    {
        public Point Point { get; }

        public CurrentPointState(Point point)
        {
            Point = point;
        }        
    }
}