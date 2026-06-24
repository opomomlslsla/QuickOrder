using QuickOrder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using QuickOrder.Domain.Interfaces;
using QuickOrder.Domain.Model;

namespace QuickOrder.Infrastructure.Repositories.Common;

internal abstract class BaseRepository<TEntity>(Context context) : IRepository<TEntity> where TEntity : BaseEntity
{
    public virtual async Task<Guid> AddAsync(TEntity entity)
    {
        var entityEntry = await context.Set<TEntity>().AddAsync(entity);
        return entityEntry.Entity.Id;
    }
    public virtual async Task<ICollection<TEntity>> GetAsync(
    Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken)
    {
        var query = context.Set<TEntity>().AsQueryable().AsNoTracking();
        if (predicate == null)
            return new List<TEntity>();
        return await query.Where(predicate).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public virtual async Task<ICollection<TEntity>> GetWithPagination(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>()
            .AsNoTracking()
            .OrderByDescending(o => o.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var  query = context.Set<TEntity>().AsQueryable();
        return await query.FirstOrDefaultAsync(predicate,cancellationToken);
    }
    public virtual void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
    public virtual void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}