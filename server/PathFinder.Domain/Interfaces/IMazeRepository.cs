namespace PathFinder.Domain.Interfaces
{
    public interface IMazeRepository
    {
        void Add(string name, int[,] grid);
        int[,] Get(string name);
    }
}