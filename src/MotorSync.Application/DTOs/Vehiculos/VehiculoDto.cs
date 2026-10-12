namespace MotorSync.Application.DTOs.Vehiculos;

public sealed record VehiculoDto(
    int IdVehiculo,
    int IdCliente,
    string Placa,
    string Marca,
    string Modelo,
    short? Anio,
    string? Vin,
    int Kilometraje,
    string Estado);
