using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models
{
    public class MazeCreationFactoryMock : IMazeCreationFactory
    {
        public int[,] Create(string name)
        {
            return new int[3, 3];
        }
    }
}