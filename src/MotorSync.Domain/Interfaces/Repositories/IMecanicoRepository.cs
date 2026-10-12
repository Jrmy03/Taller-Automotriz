using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IMecanicoRepository : IRepository<Mecanico>
{
    Task<IReadOnlyList<Mecanico>> GetActivosAsync(CancellationToken cancellationToken = default);
    Task<Mecanico?> GetConEspecialidadesAsync(int idMecanico, CancellationToken cancellationToken = default);
}
