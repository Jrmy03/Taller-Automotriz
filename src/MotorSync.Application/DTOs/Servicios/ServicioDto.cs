namespace MotorSync.Application.DTOs.Servicios;

public sealed record ServicioDto(
    int IdServicio,
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal CostoManoObra,
    int? TiempoEstimado,
    string Estado);
