using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IRepuestoRepository : IRepository<Repuesto>
{
    Task<Repuesto?> GetByCodigoParteAsync(string codigoParte, CancellationToken cancellationToken = default);

    /// <summary>Repuestos activos cuyo stock actual es menor o igual al stock mínimo.</summary>
    Task<IReadOnlyList<Repuesto>> GetBajoStockAsync(CancellationToken cancellationToken = default);
}
