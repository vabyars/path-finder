using System.Drawing;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public abstract class TestGrid
    {
        public abstract Grid Grid { get; }
        public abstract Point Start { get; }
        public abstract Point Goal { get; }
    }
}