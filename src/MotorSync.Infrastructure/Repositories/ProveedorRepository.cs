using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class ProveedorRepository(TallerAutomotrizDbContext context)
    : Repository<Proveedor>(context), IProveedorRepository
{
    public async Task<Proveedor?> GetByIdentificacionJuridicaAsync(string identificacionJuridica, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.IdentificacionJuridica == identificacionJuridica, cancellationToken);
    }

    public async Task<IReadOnlyList<Proveedor>> GetActivosAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(p => p.Estado == EstadoRegistro.Activo)
            .OrderBy(p => p.RazonSocial)
            .ToListAsync(cancellationToken);
    }
}
