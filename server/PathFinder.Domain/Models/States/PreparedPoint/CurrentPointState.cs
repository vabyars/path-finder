using System.Drawing;

namespace PathFinder.Domain.Models.States.PreparedPoint
{
    public class CurrentPointState : IState
    {
        public Point PreparedPoint { get; set; }
    }
}