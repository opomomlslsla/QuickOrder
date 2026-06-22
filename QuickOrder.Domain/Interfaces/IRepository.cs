using System.Linq.Expressions;
using QuickOrder.Domain.Model;

namespace QuickOrder.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<Guid> AddAsync(TEntity entity);
    Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<ICollection<TEntity>> GetWithPagination(int page, int pageSize);
    Task SaveChangesAsync();
}