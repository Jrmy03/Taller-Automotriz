using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Servicios;

public sealed class RegistrarServicioDto
{
    [Required(ErrorMessage = "El código de servicio es obligatorio.")]
    [StringLength(30)]
    public string Codigo { get; init; } = string.Empty;

    [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; init; } = string.Empty;

    [StringLength(250)]
    public string? Descripcion { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "El costo de mano de obra no puede ser negativo.")]
    public decimal CostoManoObra { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El tiempo estimado no puede ser negativo.")]
    public int? TiempoEstimado { get; init; }
}
