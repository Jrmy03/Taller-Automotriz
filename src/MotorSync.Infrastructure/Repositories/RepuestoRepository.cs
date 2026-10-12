using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class RepuestoRepository(TallerAutomotrizDbContext context)
    : Repository<Repuesto>(context), IRepuestoRepository
{
    public async Task<Repuesto?> GetByCodigoParteAsync(string codigoParte, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(r => r.CodigoParte == codigoParte, cancellationToken);
    }

    public async Task<IReadOnlyList<Repuesto>> GetBajoStockAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(r => r.Estado == EstadoRegistro.Activo && r.StockActual <= r.StockMinimo)
            .OrderBy(r => r.Nombre)
            .ToListAsync(cancellationToken);
    }
}
