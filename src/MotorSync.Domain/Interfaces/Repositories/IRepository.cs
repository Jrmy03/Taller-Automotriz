namespace MotorSync.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato genérico de persistencia. Las operaciones de escritura sólo marcan cambios;
/// la confirmación se hace mediante <see cref="IUnitOfWork"/>.
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Remove(T entity);
}
