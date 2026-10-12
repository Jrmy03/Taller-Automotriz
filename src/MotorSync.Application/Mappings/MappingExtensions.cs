using MotorSync.Application.DTOs.Clientes;
using MotorSync.Application.DTOs.Vehiculos;
using MotorSync.Domain.Entities;

namespace MotorSync.Application.Mappings;

/// <summary>Mapeos manuales Entidad -> DTO (sin librerías externas).</summary>
public static class MappingExtensions
{
    public static ClienteDto ToDto(this Cliente cliente) => new(
        cliente.IdCliente,
        cliente.Identificacion,
        cliente.NombreCompleto,
        cliente.Telefono,
        cliente.Correo,
        cliente.Estado);

    public static VehiculoDto ToDto(this Vehiculo vehiculo) => new(
        vehiculo.IdVehiculo,
        vehiculo.IdCliente,
        vehiculo.Placa,
        vehiculo.Marca,
        vehiculo.Modelo,
        vehiculo.Anio,
        vehiculo.Vin,
        vehiculo.Kilometraje,
        vehiculo.Estado);
}
