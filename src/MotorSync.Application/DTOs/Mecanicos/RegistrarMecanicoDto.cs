using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Mecanicos;

public sealed class RegistrarMecanicoDto
{
    [Required(ErrorMessage = "El nombre del mecánico es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; init; } = string.Empty;

    [StringLength(20)]
    public string? Telefono { get; init; }

    public List<int> IdsEspecialidades { get; init; } = [];
}
