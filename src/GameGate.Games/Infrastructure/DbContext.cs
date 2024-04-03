using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Game> Games { get; set; }
}