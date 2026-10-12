using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Repuestos;

public sealed class RegistrarRepuestoDto
{
    [StringLength(50)]
    public string? CodigoParte { get; init; }

    [Required(ErrorMessage = "El nombre del repuesto es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; init; } = string.Empty;

    [StringLength(80)]
    public string? Categoria { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "El costo de compra no puede ser negativo.")]
    public decimal CostoCompra { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio de venta no puede ser negativo.")]
    public decimal PrecioVenta { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock actual no puede ser negativo.")]
    public int StockActual { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
    public int StockMinimo { get; init; }
}
