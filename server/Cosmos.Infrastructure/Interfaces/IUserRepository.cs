using System;
using Cosmos.Domain.Entities;

namespace Cosmos.Infrastructure.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetActiveLogin(string email, byte[] passwordHash, CancellationToken cancellationToken = default);
}
