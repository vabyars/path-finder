using System;
using AutoMapper;
using Moq;
using NUnit.Framework;
using PathFinder.DataAccess;
using PathFinder.DataAccess.Implementations;
using PathFinder.Domain;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.MazeCreation;
using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Test.MazeServiceTests
{
    [TestFixture]
    public class MazeServiceTest
    {
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(x => x.AddProfile<DataAccessMappingProfile>()));
        private readonly GridWithStartAndEnd defaultGrid = new() {Maze = new int[,] { }};
        
        [Test]
        public void Add_ToRepositoryTwice_ThrowsException()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.GetAvailableNames())
                .Returns(() => new [] { "Kruskal"});

            var service = new MazeService(new MazeRepository(), creationFactory.Object);
            
            service.Add("test", new GridWithStartAndEnd{Maze = new int[,]{}});
            Assert.Throws<ArgumentException>(() => service.Add("test", defaultGrid));
        }

        [Test]
        public void Add_ToRepositoryWhenExistsInFactory_ThrowsException()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.GetAvailableNames())
                .Returns(() => new [] { "Kruskal"});

            var service = new MazeService(new MazeRepository(), creationFactory.Object);
            
            Assert.Throws<ArgumentException>(() => service.Add("Kruskal", defaultGrid));
        }

        [Test]
        public void Get_AvailableNames_Names()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.GetAvailableNames())
                .Returns(() => new [] { "Kruskal"});

            var repository = new Mock<IMazeRepository>();
            repository.Setup(x => x.GetMazesNames())
                .Returns(() => new[] {"test", "test2"});

            var service = new MazeService(repository.Object, creationFactory.Object);
            
            CollectionAssert.AreEquivalent(new [] {"Kruskal", "test", "test2"}, service.GetAvailableNames());
        }

        [Test]
        public void Get_MazeWhichNotExists_ThrowsException()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(() => null);

            var repository = new Mock<IMazeRepository>();
            repository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(() => null);
            
            var service = new MazeService(repository.Object, creationFactory.Object);
            Assert.Throws<ArgumentException>(() => service.Get("test"));
        }

        [Test]
        public void Get_MazeFromFactory()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.Create("test"))
                .Returns(() => new GridWithStartAndEnd { Maze = new int[1, 1] });

            var repository = new Mock<IMazeRepository>();
            repository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(() => null);
            
            var service = new MazeService(repository.Object, creationFactory.Object);
            CollectionAssert.AreEquivalent(new int[1, 1], service.Get("test").Maze);
        }

        [Test]
        public void Get_MazeFromRepository()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(() => null);

            var repository = new Mock<IMazeRepository>();
            repository.Setup(x => x.Get("test"))
                .Returns(() => new GridWithStartAndEnd { Maze = new int[1, 1] });
            
            var service = new MazeService(repository.Object, creationFactory.Object);
            CollectionAssert.AreEquivalent(new int[1, 1], service.Get("test").Maze);
        }

        [Test]
        public void Get_MazeWhichExistsInRepositoryAndInFactory_RepositoryVariant()
        {
            var creationFactory = new Mock<IMazeCreationFactory>();
            creationFactory.Setup(x => x.Create("test"))
                .Returns(() => new GridWithStartAndEnd { Maze = new int[2, 2] });

            var repository = new Mock<IMazeRepository>();
            repository.Setup(x => x.Get("test"))
                .Returns(() => new GridWithStartAndEnd { Maze = new int[1, 1] });
            
            var service = new MazeService(repository.Object, creationFactory.Object);
            CollectionAssert.AreEquivalent(new int[1, 1], service.Get("test").Maze);
        }
    }
}