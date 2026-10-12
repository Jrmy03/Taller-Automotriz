using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla FACTURA (relación 1 a 1 con ORDEN_TRABAJO).</summary>
public class Factura
{
    public int IdFactura { get; set; }
    public int IdOt { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Impuesto { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = EstadoFactura.Pendiente;

    // Navegación
    public OrdenTrabajo OrdenTrabajo { get; set; } = null!;
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    // Comportamiento de dominio
    public decimal TotalPagado => Pagos
        .Where(p => p.Estado == EstadoPago.Registrado)
        .Sum(p => p.Monto);

    public decimal SaldoPendiente => Total - TotalPagado;

    public void CalcularTotales(decimal subtotal, decimal porcentajeImpuesto)
    {
        Subtotal = Math.Round(subtotal, 2);
        Impuesto = Math.Round(subtotal * porcentajeImpuesto, 2);
        Total = Subtotal + Impuesto;
    }
}
