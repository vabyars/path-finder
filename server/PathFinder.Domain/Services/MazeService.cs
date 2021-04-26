using System;
using System.Linq;
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

        public int[,] Get(string name)
        {
            if (_repository.TryGetValue(name, out var maze))
                return maze;
            var generatedMaze = _mazeCreationFactory.Create(name);
            if (generatedMaze != null)
                return generatedMaze;
            throw new ArgumentException($"maze not found {name}");
        }

        public string[] GetAvailableNames()
        {
            return _mazeCreationFactory.GetAvailableNames()
                .Concat(_repository.GetMazesNames())
                .ToArray();
        }
    }
}