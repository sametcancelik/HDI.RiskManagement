using System.Linq.Expressions;

namespace HDI.Application.Interfaces.Persistence.Repositories;

public interface IGenericRepository<T, TId> where T : class
{
    Task<T?> GetByIdAsync(TId id, bool disableTracking = true);

    Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        bool disableTracking = true,
        params Expression<Func<T, object>>[] includes);

    Task<List<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        bool disableTracking = true,
        params Expression<Func<T, object>>[] includes);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}