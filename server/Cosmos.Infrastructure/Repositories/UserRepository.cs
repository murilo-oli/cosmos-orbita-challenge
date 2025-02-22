using System;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Context;
using Cosmos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(CosmosDbContext context) : base(context){}
    public async Task<User?> GetActiveLogin(string email, byte[] passwordHash, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().AsNoTracking()
                            .Where(us => us.Email == email && us.Password == passwordHash && us.IsActive)
                            .FirstOrDefaultAsync(cancellationToken);
    }
}
