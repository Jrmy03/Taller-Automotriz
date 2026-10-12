using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IFacturaRepository : IRepository<Factura>
{
    Task<Factura?> GetByNumeroAsync(string numeroFactura, CancellationToken cancellationToken = default);
    Task<Factura?> GetByOrdenTrabajoAsync(int idOt, CancellationToken cancellationToken = default);
}
