using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class VehiculoRepository(TallerAutomotrizDbContext context)
    : Repository<Vehiculo>(context), IVehiculoRepository
{
    public async Task<Vehiculo?> GetByPlacaAsync(string placa, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(v => v.Cliente)
            .FirstOrDefaultAsync(v => v.Placa == placa, cancellationToken);
    }

    public async Task<bool> ExistsByPlacaAsync(string placa, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(v => v.Placa == placa, cancellationToken);
    }

    public async Task<bool> ExistsByVinAsync(string vin, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(v => v.Vin == vin, cancellationToken);
    }

    public async Task<IReadOnlyList<Vehiculo>> GetByClienteAsync(int idCliente, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(v => v.IdCliente == idCliente)
            .OrderBy(v => v.Placa)
            .ToListAsync(cancellationToken);
    }
}
