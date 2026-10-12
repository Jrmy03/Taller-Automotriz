using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla PAGO.</summary>
public class Pago
{
    public int IdPago { get; set; }
    public int IdFactura { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string Estado { get; set; } = EstadoPago.Registrado;

    // Navegación
    public Factura Factura { get; set; } = null!;
}
