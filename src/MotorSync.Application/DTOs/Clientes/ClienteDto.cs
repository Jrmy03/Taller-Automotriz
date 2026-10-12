namespace MotorSync.Application.DTOs.Clientes;

public sealed record ClienteDto(
    int IdCliente,
    string Identificacion,
    string NombreCompleto,
    string? Telefono,
    string? Correo,
    string Estado);
