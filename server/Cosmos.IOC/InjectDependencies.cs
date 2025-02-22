using Cosmos.Application.Interfaces;
using Cosmos.Application.Services;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Interfaces;
using Cosmos.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace Cosmos.IOC;

public static class InjectDependencies
{
    public static IServiceCollection Inject(this IServiceCollection services)
    {
        #region Apply Migrations
        #endregion

        #region User
        services.AddScoped(typeof(IUserService), typeof(UserService));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        #endregion

        #region Student
        services.AddScoped(typeof(IStudentService), typeof(StudentService));
        services.AddScoped(typeof(IBaseRepository<Student>), typeof(BaseRepository<Student>));
        #endregion

        return services;
    }
}
