using System;
using System.Threading.Tasks;

namespace PathFinder.DataAccess1
{
    public static class MazeRepositoryExtensions
    {
        public static bool TryGetValue(this IMazeRepository repository, string name, out int[,] value)
        {
            value = repository.Get(name);
            return value != null;
        }

        public static async Task<bool> TryGetValueAsync(this IMazeRepository repository, string name, Action<int[,]> setValue)
        {
            var value = await repository.GetAsync(name);
            setValue(value);
            return value != null;
        }
    }
}