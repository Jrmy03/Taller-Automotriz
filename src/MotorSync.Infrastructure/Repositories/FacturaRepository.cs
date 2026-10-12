using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class FacturaRepository(TallerAutomotrizDbContext context)
    : Repository<Factura>(context), IFacturaRepository
{
    public async Task<Factura?> GetByNumeroAsync(string numeroFactura, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(f => f.Pagos)
            .FirstOrDefaultAsync(f => f.NumeroFactura == numeroFactura, cancellationToken);
    }

    public async Task<Factura?> GetByOrdenTrabajoAsync(int idOt, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(f => f.Pagos)
            .FirstOrDefaultAsync(f => f.IdOt == idOt, cancellationToken);
    }
}
