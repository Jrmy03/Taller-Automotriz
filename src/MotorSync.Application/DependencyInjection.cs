using Microsoft.Extensions.DependencyInjection;
using MotorSync.Application.UseCases.Clientes.BuscarClientePorIdentificacion;
using MotorSync.Application.UseCases.Clientes.ConsultarClientes;
using MotorSync.Application.UseCases.Clientes.RegistrarCliente;
using MotorSync.Application.UseCases.Facturacion.GenerarFactura;
using MotorSync.Application.UseCases.Mecanicos.RegistrarMecanico;
using MotorSync.Application.UseCases.OrdenesTrabajo.RegistrarOrdenTrabajo;
using MotorSync.Application.UseCases.Proveedores.RegistrarProveedor;
using MotorSync.Application.UseCases.Repuestos.RegistrarRepuesto;
using MotorSync.Application.UseCases.Servicios.RegistrarServicio;
using MotorSync.Application.UseCases.Vehiculos.RegistrarVehiculo;

namespace MotorSync.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Clientes
        services.AddScoped<IRegistrarClienteUseCase, RegistrarClienteUseCase>();
        services.AddScoped<IConsultarClientesUseCase, ConsultarClientesUseCase>();
        services.AddScoped<IBuscarClientePorIdentificacionUseCase, BuscarClientePorIdentificacionUseCase>();

        // Vehículos
        services.AddScoped<IRegistrarVehiculoUseCase, RegistrarVehiculoUseCase>();

        // Mecánicos
        services.AddScoped<IRegistrarMecanicoUseCase, RegistrarMecanicoUseCase>();

        // Servicios
        services.AddScoped<IRegistrarServicioUseCase, RegistrarServicioUseCase>();

        // Repuestos
        services.AddScoped<IRegistrarRepuestoUseCase, RegistrarRepuestoUseCase>();

        // Proveedores
        services.AddScoped<IRegistrarProveedorUseCase, RegistrarProveedorUseCase>();

        // Órdenes de Trabajo
        services.AddScoped<IRegistrarOrdenTrabajoUseCase, RegistrarOrdenTrabajoUseCase>();

        // Facturación
        services.AddScoped<IGenerarFacturaUseCase, GenerarFacturaUseCase>();

        return services;
    }
}
