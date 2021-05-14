namespace PathFinder.Domain.Interfaces
{
    public interface IMazeGenerator
    {
        int[,] Create(int width, int height);
    }
}