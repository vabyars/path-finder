using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Test.AlgorithmsTests.TestGrids
{
    public class GridWithNoWay : TestGrid
    {
        public override Grid Grid { get; } = new(new[,]
        {
            {1, 1, -1, 1, 1, 1},
            {1, 1, -1, 1, 1, 1},
            {-1, -1, -1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1}
        });

        public override Point Start { get; } = new (0, 0);
        public override Point Goal { get; } = new(4, 4);
    }
}