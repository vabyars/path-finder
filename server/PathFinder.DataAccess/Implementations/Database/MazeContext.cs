using Microsoft.EntityFrameworkCore;
using PathFinder.DataAccess1.Entities;

namespace PathFinder.DataAccess1.Implementations.Database
{
    public class MazeContext : DbContext
    {
        public DbSet<Grid> Grids { get; set; }
        
        public MazeContext(DbContextOptions<MazeContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var intValueConverter = new IntTwoDimensionsArrayToStringValueConverter();
            var pointConverter = new PointToStringConverter();

            modelBuilder
                .Entity<Grid>()
                .Property(e => e.Maze)
                .HasConversion(intValueConverter);

            modelBuilder
                .Entity<Grid>()
                .Property(x => x.Start)
                .HasConversion(pointConverter);
            
            modelBuilder
                .Entity<Grid>()
                .Property(x => x.End)
                .HasConversion(pointConverter);
        }
    }
}