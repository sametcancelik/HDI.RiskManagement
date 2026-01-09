using HDI.Application.Interfaces.Persistence;
using HDI.Application.Interfaces.Persistence.Repositories;
using HDI.Persistence.Contexts;
using HDI.Persistence.Repositories;

namespace HDI.Persistence;
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly Dictionary<string, object> _repositories = new();

    public IGenericRepository<T, TId> Repository<T, TId>() where T : class
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new GenericRepository<T, TId>(_context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T, TId>)_repositories[type];
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RollbackAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}