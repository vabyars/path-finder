namespace PathFinder.Domain.Interfaces
{
    public interface IMazeService
    {
        void Add(string name, int[,] grid);
        int[,] Get(string name, bool fromDb);
    }
}