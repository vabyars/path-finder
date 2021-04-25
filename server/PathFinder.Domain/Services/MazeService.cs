using System;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Services
{
    public class MazeService : IMazeService
    {
        private readonly IMazeRepository _repository;
        private readonly IMazeCreationFactory _mazeCreationFactory;

        public MazeService(IMazeRepository repository, IMazeCreationFactory mazeCreationFactory)
        {
            _repository = repository;
            _mazeCreationFactory = mazeCreationFactory;
        }
        
        public void Add(string name, int[,] grid)
        {
            _repository.Add(name, grid);
        }

        public int[,] Get(string name, bool fromDb)
        {
            return fromDb ? GetFromRepository(name) : GenerateMaze(name);
        }

        private int[,] GetFromRepository(string name)
        {
            try
            {
                return _repository.Get(name);
            }
            catch (Exception)
            {
                throw new ArgumentException($"maze not found {name}");
            }
        }

        private int[,] GenerateMaze(string name)
        {
            return _mazeCreationFactory.Create(name);
        }
        
    }
}