namespace MotorSync.Application.DTOs.Mecanicos;

public sealed record MecanicoDto(
    int IdMecanico,
    string Nombre,
    string? Telefono,
    string Estado,
    IReadOnlyList<string> Especialidades);
