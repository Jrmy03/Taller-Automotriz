using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorSync.Application.Interfaces.Services;
using MotorSync.Domain.Interfaces.Repositories;
using MotorSync.Infrastructure.Context;
using MotorSync.Infrastructure.Repositories;
using MotorSync.Infrastructure.Services;

namespace MotorSync.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TallerAutomotrizConnection")
            ?? "Server=(localdb)\\mssqllocaldb;Database=TallerAutomotriz;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

        // DbContext
        services.AddDbContext<TallerAutomotrizDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositorios de datos
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IMecanicoRepository, MecanicoRepository>();
        services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
        services.AddScoped<IProveedorRepository, ProveedorRepository>();
        services.AddScoped<IServicioRepository, ServicioRepository>();
        services.AddScoped<IRepuestoRepository, RepuestoRepository>();
        services.AddScoped<IOrdenTrabajoRepository, OrdenTrabajoRepository>();
        services.AddScoped<IFacturaRepository, FacturaRepository>();

        // Servicios de infraestructura
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
