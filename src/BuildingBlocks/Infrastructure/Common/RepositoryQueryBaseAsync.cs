using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class RepositoryQueryBaseAsync<T, TK, TContext> : IRepositoryQueryBase<T, TK, TContext>
    where T : EntityBase<TK>
    where TContext : DbContext
{
    private readonly TContext _dbContext;

    protected RepositoryQueryBaseAsync(TContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges
            ? _dbContext.Set<T>().Where(expression).AsNoTracking()
            : _dbContext.Set<T>().Where(expression);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(TK id) => await FindByCondition(x => x.Id!.Equals(id)).FirstOrDefaultAsync();

    public async Task<T?> GetByIdAsync(TK id, params Expression<Func<T, object>>[] includeProperties)
    {
        return await FindByCondition(x => x.Id!.Equals(id), false, includeProperties)
            .FirstOrDefaultAsync();
    }
}