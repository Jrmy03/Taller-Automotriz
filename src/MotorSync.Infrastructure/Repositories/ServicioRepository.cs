using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class ServicioRepository(TallerAutomotrizDbContext context)
    : Repository<Servicio>(context), IServicioRepository
{
    public async Task<Servicio?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.Codigo == codigo, cancellationToken);
    }

    public async Task<IReadOnlyList<Servicio>> GetActivosAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(s => s.Estado == EstadoRegistro.Activo)
            .OrderBy(s => s.Nombre)
            .ToListAsync(cancellationToken);
    }
}
