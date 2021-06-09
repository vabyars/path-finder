namespace PathFinder.Domain.Models.MazeCreation.MazeGenerators
{
    public interface IMazeGenerator
    {
        string Name { get; }
        int[,] Create(int resultWidth, int resultHeight);
    }
}