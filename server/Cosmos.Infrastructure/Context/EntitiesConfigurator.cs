using Cosmos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Context;

public class EntitiesConfigurator
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        #region Student
        modelBuilder.Entity<Student>().HasKey(st => st.Id);

        modelBuilder.Entity<Student>()
        .HasIndex(st => st.RA)
        .IsUnique();
        #endregion

        #region User
        modelBuilder.Entity<User>().HasKey(us => us.Id);
        #endregion
    }
}
