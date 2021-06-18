using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.DataAccess1;
using PathFinder.Domain.Models.MazeCreation;

namespace PathFinder.Domain.Services.MazeService
{
    public class MazeService : IMazeService
    {
        private readonly IMazeRepository repository;
        private readonly IMazeCreationFactory mazeCreationFactory;

        public MazeService(IMazeRepository repository, IMazeCreationFactory mazeCreationFactory)
        {
            this.repository = repository;
            this.mazeCreationFactory = mazeCreationFactory;
        }

        public void Add(string name, int[,] grid)
        {
            if (MazeExists(name))
                throw new ArgumentException($"maze with name \"{name}\" has already exists");
            repository.AddAsync(name, grid);
        }

        private bool MazeExists(string name)
        {
            var existsInRepository = repository.TryGetValue(name, out _);
            var existsInFactory = mazeCreationFactory.GetAvailableNames().Contains(name);
            return existsInRepository || existsInFactory;
        }

        public int[,] Get(string name)
        {
            if (repository.TryGetValue(name, out var maze))
                return maze;
            var generatedMaze = mazeCreationFactory.Create(name);
            if (generatedMaze != null)
                return generatedMaze;
            throw new ArgumentException($"maze not found {name}");
        }

        public IEnumerable<string> GetAvailableNames()
        {
            return mazeCreationFactory.GetAvailableNames()
                .Concat(repository.GetMazesNames());
        }
        
        public async Task AddAsync(string name, int[,] grid)
        {
            if (await MazeExistsAsync(name))
                throw new ArgumentException($"maze with name \"{name}\" has already exists");
            await repository.AddAsync(name, grid);
        }
        
        private async Task<bool> MazeExistsAsync(string name)
        {
            var existsInRepository = await repository.TryGetValueAsync(name, _ => { });
            var existsInFactory = mazeCreationFactory.GetAvailableNames().Contains(name);
            return existsInRepository || existsInFactory;
        }
        
        public async Task<int[,]> GetAsync(string name)
        {
            int[,] maze = { };
            if (await repository.TryGetValueAsync(name, value => maze = value))
                return maze;
            var generatedMaze = mazeCreationFactory.Create(name);
            if (generatedMaze != null)
                return generatedMaze;
            throw new ArgumentException($"maze not found {name}");
        }
        
        public async Task<IEnumerable<string>> GetAvailableNamesAsync()
        {
            return mazeCreationFactory.GetAvailableNames()
                .Concat(await repository.GetMazesNamesAsync());
        }
    }
}