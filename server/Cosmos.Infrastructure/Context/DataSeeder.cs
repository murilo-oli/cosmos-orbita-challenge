using System;
using Cosmos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Context;

public class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Name = "Administrator",
            Email = "admin@cosmos.com",
            AvatarPath = "",
            Role = Domain.Enums.EUserRole.Admin,
            Password = "3C42822BB09F8BD829A0460F12E19DF3-085E00DFD40DB3AE",
        });
    }
}
