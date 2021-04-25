namespace PathFinder.Domain.Interfaces
{
    public interface IMazeCreationFactory
    {
        int[,] Create(string name);
    }
}