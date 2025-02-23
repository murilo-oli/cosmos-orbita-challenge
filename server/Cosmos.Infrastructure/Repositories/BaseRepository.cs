using System.Linq.Expressions;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Context;
using Cosmos.Infrastructure.Interfaces;
using Cosmos.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Cosmos.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{

    protected CosmosDbContext _context;

    public BaseRepository(CosmosDbContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity, CancellationToken cancellationToken = default)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<List<T>> AddRange(List<T> entities, CancellationToken cancellationToken = default)
    {
        _context.AddRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task<T?> GetFiltered(Func<IQueryable<T>, IQueryable<T>?> expression, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>().AsNoTracking();

        if (expression != null)
        {
            query = expression(query) ?? query;
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetById(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllFiltered(Func<IQueryable<T>, IQueryable<T>?> expression, Pagination? pagination, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>().AsNoTracking();

        if (expression != null)
        {
            query = expression(query) ?? query;
        }

        if (pagination != null)
        {
            query = query.Skip(pagination.PageSize * (pagination.CurrentPage - 1))
                        .Take(pagination.PageSize);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAll(Pagination? pagination, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>().AsNoTracking();

        if (pagination != null)
        {
            query = query.Skip(pagination.PageSize * (pagination.CurrentPage - 1))
                        .Take(pagination.PageSize);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T> Remove(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task RemoveRange(List<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateRange(List<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exist(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().AsNoTracking().AnyAsync(expression, cancellationToken);
    }
}
