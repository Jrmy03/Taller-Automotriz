using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class ClienteRepository(TallerAutomotrizDbContext context)
    : Repository<Cliente>(context), IClienteRepository
{
    public async Task<Cliente?> GetByIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(c => c.Identificacion == identificacion, cancellationToken);
    }

    public async Task<bool> ExistsByIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AnyAsync(c => c.Identificacion == identificacion, cancellationToken);
    }

    public async Task<IReadOnlyList<Cliente>> GetActivosAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.Estado == EstadoRegistro.Activo)
            .OrderBy(c => c.NombreCompleto)
            .ToListAsync(cancellationToken);
    }
}
