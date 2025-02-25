using System;
using Cosmos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Context;

public class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        DotNetEnv.Env.Load("../../");

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Name = "Administrator",
            Email = "admin@cosmos.com",
            AvatarPath = "",
            Role = Domain.Enums.EUserRole.Admin,
            Password = Environment.GetEnvironmentVariable("DEFAULT_FIRST_PASS")!,
        });
    }
}
