using System.Collections.Generic;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models
{
    public class MazeCreationFactoryTestRealization : IMazeCreationFactory
    {//по факту, в нормальной реализации, надо принимать в конструкторе массив из IMazeGenerator, который будет инжектится,
     //далее, сам этот класс тоже инжектить
        public IEnumerable<string> GetAvailableNames()
        {
            return new[] {"random", "special"};
        }

        public int[,] Create(string name)
        {
            return new int[3, 3];
        }
    }
}