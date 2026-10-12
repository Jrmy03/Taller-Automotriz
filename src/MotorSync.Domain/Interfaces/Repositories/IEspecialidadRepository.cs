using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IEspecialidadRepository : IRepository<Especialidad>
{
    Task<bool> ExistsByNombreAsync(string nombre, CancellationToken cancellationToken = default);
}
