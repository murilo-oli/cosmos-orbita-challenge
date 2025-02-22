using Cosmos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Context;

public class CosmosDbContext(DbContextOptions<CosmosDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students {get;set;}
    public DbSet<User> Users {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntitiesConfigurator.Configure(modelBuilder);
        DataSeeder.Seed(modelBuilder);
    }
}
