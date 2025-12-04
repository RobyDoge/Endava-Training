using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data;

public class FootballLeagueDbContext : DbContext
{
    public DbSet<Team> Teams { get; set; }
    public DbSet<Coach> Coaches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeague_EFCore; Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var createdAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Team>().HasData(
            new Team
            {
                Id = 1,
                Name = "Petrolul",
                CreatedDate = createdAt,
            },
            new Team
            {
                Id = 2,
                Name = "Dinamo",
                CreatedDate = createdAt,
            },
            new Team
            {
                Id = 3,
                Name = "FCSB",
                CreatedDate = createdAt,
            }
            );
    }
}