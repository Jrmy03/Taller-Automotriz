using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>
/// Tabla DETALLE_OT. Cada detalle es un Servicio o un Repuesto, nunca ambos (CK_DETALLE_TIPO).
/// </summary>
public class DetalleOt
{
    public int IdDetalle { get; set; }
    public int IdOt { get; set; }
    public int? IdServicio { get; set; }
    public int? IdRepuesto { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    // Navegación
    public OrdenTrabajo OrdenTrabajo { get; set; } = null!;
    public Servicio? Servicio { get; set; }
    public Repuesto? Repuesto { get; set; }

    // Valor calculado (no se mapea a columna)
    public decimal Subtotal => Cantidad * PrecioUnitario;

    // Fábricas que garantizan la regla de negocio de CK_DETALLE_TIPO
    public static DetalleOt CrearDeServicio(int idServicio, int cantidad, decimal precioUnitario) => new()
    {
        Tipo = TipoDetalleOt.Servicio,
        IdServicio = idServicio,
        IdRepuesto = null,
        Cantidad = cantidad,
        PrecioUnitario = precioUnitario
    };

    public static DetalleOt CrearDeRepuesto(int idRepuesto, int cantidad, decimal precioUnitario) => new()
    {
        Tipo = TipoDetalleOt.Repuesto,
        IdRepuesto = idRepuesto,
        IdServicio = null,
        Cantidad = cantidad,
        PrecioUnitario = precioUnitario
    };
}
