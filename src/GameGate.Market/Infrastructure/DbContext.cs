using Domain;
using Domain.Games;
using Domain.Users;
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
        modelBuilder.Entity<Buyer>().HasKey(x => x.UserId);
        modelBuilder.Entity<Order>().HasKey(x => x.OrderId);
        modelBuilder.Entity<Game>().HasKey(x => x.GameName);
        modelBuilder.Entity<Game>().OwnsOne(x => x.Filters);
        modelBuilder.Entity<Game>().OwnsOne(x => x.Price);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Order> Orders { get; set; }
}