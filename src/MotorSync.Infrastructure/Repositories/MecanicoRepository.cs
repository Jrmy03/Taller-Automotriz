using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class MecanicoRepository(TallerAutomotrizDbContext context)
    : Repository<Mecanico>(context), IMecanicoRepository
{
    public async Task<IReadOnlyList<Mecanico>> GetActivosAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(m => m.Estado == EstadoRegistro.Activo)
            .OrderBy(m => m.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<Mecanico?> GetConEspecialidadesAsync(int idMecanico, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(m => m.Especialidades)
            .FirstOrDefaultAsync(m => m.IdMecanico == idMecanico, cancellationToken);
    }
}
