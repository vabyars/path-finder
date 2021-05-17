using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.DataAccess1;
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
            if (IsMazeExists(name))
                throw new ArgumentException($"maze with name \"{name}\" has already exists");
            _repository.Add(name, grid);
        }

        private bool IsMazeExists(string name)
        {
            var existsInRepository = _repository.TryGetValue(name, out var maze);
            var existsInFactory = _mazeCreationFactory.GetAvailableNames().Contains(name);
            return existsInRepository || existsInFactory;
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

        public IEnumerable<string> GetAvailableNames()
        {
            return _mazeCreationFactory.GetAvailableNames()
                .Concat(_repository.GetMazesNames());
        }
    }
}