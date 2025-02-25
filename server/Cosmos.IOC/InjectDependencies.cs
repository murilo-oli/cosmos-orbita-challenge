using System.Text;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Services;
using Cosmos.Domain.Entities;
using Cosmos.Domain.Enums;
using Cosmos.Infrastructure.Context;
using Cosmos.Infrastructure.Interfaces;
using Cosmos.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
namespace Cosmos.IOC;

public static class InjectDependencies
{
    public static IServiceCollection Inject(this IServiceCollection services)
    {
        DotNetEnv.Env.Load("../");

        #region Auto Apply Migrations
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CosmosDbContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            bool isDatabaseSeeded = dbContext.Users.Any();
            if (pendingMigrations.Any() || !isDatabaseSeeded) dbContext.Database.Migrate();
        }
        #endregion

        #region User
        services.AddScoped(typeof(IUserService), typeof(UserService));
        services.AddScoped(typeof(IBaseRepository<User>), typeof(BaseRepository<User>));
        #endregion

        #region Student
        services.AddScoped(typeof(IStudentService), typeof(StudentService));
        services.AddScoped(typeof(IBaseRepository<Student>), typeof(BaseRepository<Student>));
        #endregion

        #region Auth
        services.AddScoped(typeof(IAuthService), typeof(AuthService));
        #endregion

        #region JWT
        byte[] key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(b => {
            b.RequireHttpsMetadata = false;
            b.SaveToken = true;
            b.TokenValidationParameters = new TokenValidationParameters{
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddScoped(typeof(ITokenService), typeof(TokenService));
        #endregion

        #region Authorization
        services.AddAuthorization(opt =>
        {
            opt.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            opt.AddPolicy("Admin", policy => policy.RequireRole(EUserRole.Admin.ToString()));
            opt.AddPolicy("Manager", policy => policy.RequireRole(EUserRole.Manager.ToString()));
            opt.AddPolicy("Member", policy => policy.RequireRole(EUserRole.Member.ToString()));
        });
        #endregion

        return services;
    }
}
