using MotorSync.Domain.Entities;

namespace MotorSync.Domain.Interfaces.Repositories;

public interface IVehiculoRepository : IRepository<Vehiculo>
{
    Task<Vehiculo?> GetByPlacaAsync(string placa, CancellationToken cancellationToken = default);
    Task<bool> ExistsByPlacaAsync(string placa, CancellationToken cancellationToken = default);
    Task<bool> ExistsByVinAsync(string vin, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Vehiculo>> GetByClienteAsync(int idCliente, CancellationToken cancellationToken = default);
}
