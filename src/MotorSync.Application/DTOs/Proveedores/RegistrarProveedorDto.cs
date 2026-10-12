using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Proveedores;

public sealed class RegistrarProveedorDto
{
    [Required(ErrorMessage = "La razón social es obligatoria.")]
    [StringLength(150)]
    public string RazonSocial { get; init; } = string.Empty;

    [StringLength(30)]
    public string? IdentificacionJuridica { get; init; }

    [StringLength(20)]
    public string? Telefono { get; init; }

    [EmailAddress]
    [StringLength(150)]
    public string? Correo { get; init; }

    [StringLength(250)]
    public string? Direccion { get; init; }
}
