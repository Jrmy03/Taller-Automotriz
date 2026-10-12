using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IOrdenTrabajoRepository : IRepository<OrdenTrabajo>
{
    Task<OrdenTrabajo?> GetConDetallesAsync(int idOt, CancellationToken cancellationToken = default);

    /// <summary>Historial de órdenes de un vehículo, de la más reciente a la más antigua.</summary>
    Task<IReadOnlyList<OrdenTrabajo>> GetHistorialPorPlacaAsync(string placa, CancellationToken cancellationToken = default);
}
