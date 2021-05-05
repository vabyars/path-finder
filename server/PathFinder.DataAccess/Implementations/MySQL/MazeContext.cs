using Microsoft.EntityFrameworkCore;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1.Implementations.MySQL
{
    public class MazeContext : DbContext
    {
        public DbSet<Grid> Grids { get; set; }
        
        public MazeContext(DbContextOptions<MazeContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}