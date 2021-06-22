using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Domain.Models.MazeCreation.MazeGenerators
{
    public interface IMazeGenerator
    {
        string Name { get; }
        GridWithStartAndEnd Create(int resultWidth, int resultHeight);
    }
}