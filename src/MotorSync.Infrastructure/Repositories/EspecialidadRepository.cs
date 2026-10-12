using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;

namespace MotorSync.Infrastructure.Repositories;

public sealed class EspecialidadRepository(TallerAutomotrizDbContext context)
    : Repository<Especialidad>(context), IEspecialidadRepository
{
    public async Task<bool> ExistsByNombreAsync(string nombre, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(e => e.Nombre == nombre, cancellationToken);
    }
}
