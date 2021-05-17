namespace PathFinder.Domain.Interfaces
{
    public interface IMazeGenerator
    {
        string Name { get; }
        int[,] Create();
    }
}