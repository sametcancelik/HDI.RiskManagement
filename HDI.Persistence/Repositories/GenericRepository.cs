using HDI.Application.Interfaces.Persistence.Repositories;
using HDI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HDI.Persistence.Repositories;

public class GenericRepository<T, TId>(ApplicationDbContext context) : IGenericRepository<T, TId> where T : class
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(TId id, bool disableTracking = true)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null && disableTracking)
            _context.Entry(entity).State = EntityState.Detached;
            
        return entity;
    }

    public async Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        bool disableTracking = true,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        // Include'ları Query'ye ekleme (En sık hata yapılan yer)
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        bool disableTracking = true,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate != null) query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        => predicate != null ? await _dbSet.CountAsync(predicate) : await _dbSet.CountAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}