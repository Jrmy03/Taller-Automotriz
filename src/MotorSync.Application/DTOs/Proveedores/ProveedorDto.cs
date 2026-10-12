namespace MotorSync.Application.DTOs.Proveedores;

public sealed record ProveedorDto(
    int IdProveedor,
    string RazonSocial,
    string? IdentificacionJuridica,
    string? Telefono,
    string? Correo,
    string? Direccion,
    string Estado);
