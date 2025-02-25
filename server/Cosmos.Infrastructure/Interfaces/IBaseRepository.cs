using System;
using System.Linq.Expressions;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Utilities;

namespace Cosmos.Infrastructure.Interfaces;

public interface IBaseRepository<T> where T : Base
{
    Task<T> Add(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> AddRange(List<T> entities, CancellationToken cancellationToken = default);
    Task<T> Update(T entity, CancellationToken cancellationToken = default);
    Task UpdateRange(List<T> entities, CancellationToken cancellationToken = default);
    Task<T> Remove(T entity, CancellationToken cancellationToken = default);
    Task RemoveRange(List<T> entities, CancellationToken cancellationToken = default);
    Task<T?> GetFiltered(Func<IQueryable<T>, IQueryable<T>?> expression, CancellationToken cancellationToken = default);
    Task<T?> GetById(long id, CancellationToken cancellationToken = default);
    Task<T?> GetByInclude(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<IEnumerable<T?>> GetAllByInclude(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAll(Pagination? pagination, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllFiltered(Func<IQueryable<T>, IQueryable<T>?> expression, Pagination? pagination, CancellationToken cancellationToken = default);
    Task<bool> Exist(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}
