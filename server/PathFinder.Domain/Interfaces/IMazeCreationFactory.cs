namespace PathFinder.Domain.Interfaces
{
    public interface IMazeCreationFactory
    {
        string[] GetAvailableNames();
        int[,] Create(string name);
    }
}