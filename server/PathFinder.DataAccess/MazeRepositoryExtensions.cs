using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1
{
    public static class MazeRepositoryExtensions
    {
        public static bool TryGetValue(this IMazeRepository repository, string name, out Grid value)
        {
            value = repository.Get(name);
            return value != null;
        }
    }
}