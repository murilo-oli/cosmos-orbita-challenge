using Cosmos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Context;

public class EntitiesConfigurator
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
        .HasIndex(s => s.RA)
        .IsUnique();
    }
}
