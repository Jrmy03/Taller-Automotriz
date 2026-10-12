using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class OrdenTrabajoRepository(TallerAutomotrizDbContext context)
    : Repository<OrdenTrabajo>(context), IOrdenTrabajoRepository
{
    public async Task<OrdenTrabajo?> GetConDetallesAsync(int idOt, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(ot => ot.Vehiculo)
            .Include(ot => ot.Mecanico)
            .Include(ot => ot.Detalles)
                .ThenInclude(d => d.Servicio)
            .Include(ot => ot.Detalles)
                .ThenInclude(d => d.Repuesto)
            .FirstOrDefaultAsync(ot => ot.IdOt == idOt, cancellationToken);
    }

    public async Task<IReadOnlyList<OrdenTrabajo>> GetHistorialPorPlacaAsync(string placa, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(ot => ot.Vehiculo)
            .Include(ot => ot.Mecanico)
            .Where(ot => ot.Vehiculo.Placa == placa)
            .OrderByDescending(ot => ot.FechaApertura)
            .ToListAsync(cancellationToken);
    }
}
