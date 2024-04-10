using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>().HasKey(x => x.GameId);
        modelBuilder.Entity<Game>().OwnsOne(x => x.Price);
        modelBuilder.Entity<Game>().OwnsOne(x => x.Filters);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Game> Games { get; set; }
}