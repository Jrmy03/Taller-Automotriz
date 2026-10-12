using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Clientes;

public sealed class RegistrarClienteDto
{
    [Required(ErrorMessage = "La identificación es obligatoria.")]
    [StringLength(30)]
    public string Identificacion { get; init; } = string.Empty;

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(150)]
    public string NombreCompleto { get; init; } = string.Empty;

    [StringLength(20)]
    public string? Telefono { get; init; }

    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    [StringLength(150)]
    public string? Correo { get; init; }
}
