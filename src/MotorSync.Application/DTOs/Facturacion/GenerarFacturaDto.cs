using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.Facturacion;

public sealed class GenerarFacturaDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar una orden de trabajo válida.")]
    public int IdOt { get; init; }

    [Required, StringLength(40)]
    public string NumeroFactura { get; init; } = string.Empty;

    [Range(0, 1.0, ErrorMessage = "El porcentaje de impuesto debe estar entre 0.0 y 1.0 (ej. 0.13 para 13%).")]
    public decimal PorcentajeImpuesto { get; init; } = 0.13m;
}

public sealed class RegistrarPagoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar una factura válida.")]
    public int IdFactura { get; init; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
    public decimal Monto { get; init; }

    [Required, StringLength(30)]
    public string MetodoPago { get; init; } = "Efectivo";
}
