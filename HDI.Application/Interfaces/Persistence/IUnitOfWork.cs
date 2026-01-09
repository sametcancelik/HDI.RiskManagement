using HDI.Application.Interfaces.Persistence.Repositories;

namespace HDI.Application.Interfaces.Persistence;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T, TId> Repository<T, TId>() where T : class;
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync();
}
