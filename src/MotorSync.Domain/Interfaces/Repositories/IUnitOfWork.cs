namespace MotorSync.Domain.Interfaces.Repositories;

/// <summary>Confirma en una sola transacción los cambios registrados por los repositorios.</summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
