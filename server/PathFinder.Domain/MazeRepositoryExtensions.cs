using System;
using System.Threading.Tasks;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Domain
{
    public static class MazeRepositoryExtensions
    {
        public static bool TryGetValue(this IMazeRepository repository, string name, out GridWithStartAndEnd value)
        {
            try
            {
                value = repository.Get(name);
                return value != null;
            }
            catch (Exception)
            {
                value = null;
                return false;
            }
        }

        public static async Task<bool> TryGetValueAsync(this IMazeRepository repository, string name, Action<GridWithStartAndEnd> setValue)
        {
            try
            {
                var value = await repository.GetAsync(name);
                setValue(value);
                return value != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}