using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente?> GetByIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Cliente>> GetActivosAsync(CancellationToken cancellationToken = default);
}
