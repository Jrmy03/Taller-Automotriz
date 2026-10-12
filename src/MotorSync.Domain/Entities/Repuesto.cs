using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla REPUESTO.</summary>
public class Repuesto
{
    public int IdRepuesto { get; set; }
    public string? CodigoParte { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public decimal CostoCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public string Estado { get; set; } = EstadoRegistro.Activo;

    // Navegación
    public ICollection<ProveedorRepuesto> ProveedorRepuestos { get; set; } = new List<ProveedorRepuesto>();
    public ICollection<DetalleOt> DetallesOt { get; set; } = new List<DetalleOt>();

    // Comportamiento de dominio
    public bool RequiereReabastecimiento => StockActual <= StockMinimo;

    public void DescontarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad debe ser mayor a cero.");
        if (cantidad > StockActual)
            throw new InvalidOperationException($"Stock insuficiente para el repuesto '{Nombre}'.");

        StockActual -= cantidad;
    }

    public void AumentarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad debe ser mayor a cero.");

        StockActual += cantidad;
    }
}
