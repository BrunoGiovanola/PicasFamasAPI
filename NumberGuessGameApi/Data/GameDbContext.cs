using Microsoft.EntityFrameworkCore;
using NumberGuessGameApi.Models;
using System.Reflection.Emit;

namespace NumberGuessGameApi.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Attempt> Attempts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasMany(p => p.Games)
            .WithOne(g => g.Player)
            .HasForeignKey(g => g.PlayerId);

        modelBuilder.Entity<Game>()
            .HasMany(g => g.Attempts)
            .WithOne(a => a.Game)
            .HasForeignKey(a => a.GameId);

        base.OnModelCreating(modelBuilder);
    }
}