using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IProveedorRepository : IRepository<Proveedor>
{
    Task<Proveedor?> GetByIdentificacionJuridicaAsync(string identificacionJuridica, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Proveedor>> GetActivosAsync(CancellationToken cancellationToken = default);
}
