namespace PathFinder.Domain.Interfaces
{
    public interface IMazeService
    {
        string[] GetAvailableNames();
        void Add(string name, int[,] grid);
        int[,] Get(string name);
    }
}