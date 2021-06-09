namespace PathFinder.DataAccess1
{
    public static class MazeRepositoryExtensions
    {
        public static bool TryGetValue(this IMazeRepository repository, string name, out int[,] value)
        {
            value = repository.Get(name);
            return value != null;
        }
    }
}