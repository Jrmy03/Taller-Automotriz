using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IServicioRepository : IRepository<Servicio>
{
    Task<Servicio?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Servicio>> GetActivosAsync(CancellationToken cancellationToken = default);
}
