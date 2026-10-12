using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Vehiculos;

public sealed class RegistrarVehiculoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar un cliente válido.")]
    public int IdCliente { get; init; }

    [Required, StringLength(20)]
    public string Placa { get; init; } = string.Empty;

    [Required, StringLength(50)]
    public string Marca { get; init; } = string.Empty;

    [Required, StringLength(50)]
    public string Modelo { get; init; } = string.Empty;

    [Range(1886, 2100, ErrorMessage = "El año debe estar entre 1886 y 2100.")]
    public short? Anio { get; init; }

    [StringLength(50)]
    public string? Vin { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El kilometraje no puede ser negativo.")]
    public int Kilometraje { get; init; }
}
