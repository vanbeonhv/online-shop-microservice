using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common;

public class RepositoryBaseAsync<T, TK, TContext> : RepositoryQueryBaseAsync<T, TK, TContext>,
    IRepositoryBaseAsync<T, TK, TContext>
    where T : EntityBase<TK>
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private readonly IUnitOfWork<TContext> _unitOfWork;

    public RepositoryBaseAsync(TContext dbContext, IUnitOfWork<TContext> unitOfWork) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TK> CreateAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity.Id!;
    }

    public async Task<List<TK>> CreateListAsync(List<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        return entities.Select(x => x.Id!).ToList();
    }

    public Task UpdateAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;

        var exist = _dbContext.Set<T>().Find(entity.Id);
        if (exist == null) return Task.CompletedTask;

        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }

    public Task UpdateListAsync(List<T> entities)
    {
        foreach (var entity in entities)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) continue;

            var exist = _dbContext.Set<T>().Find(entity.Id);
            if (exist == null) continue;

            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteListAsync(List<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync() => _unitOfWork.CommitAsync();

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        var transaction = _dbContext.Database.BeginTransaction();
        return Task.FromResult(transaction);
    }

    public Task EndTransactionAsync()
    {
        var transaction = _dbContext.Database.CurrentTransaction;
        if (transaction == null) return Task.CompletedTask;
        transaction.Commit();
        transaction.Dispose();

        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync()
    {
        var transaction = _dbContext.Database.CurrentTransaction;
        if (transaction == null) return Task.CompletedTask;
        transaction.Rollback();
        transaction.Dispose();

        return Task.CompletedTask;
    }
}