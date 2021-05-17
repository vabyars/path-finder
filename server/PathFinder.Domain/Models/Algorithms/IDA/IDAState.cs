using System.Drawing;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Algorithms.IDA
{
    public class IDAState : State
    {
        public Point Point { get; set; }
        
        public string Name { get; set; }
    }
}