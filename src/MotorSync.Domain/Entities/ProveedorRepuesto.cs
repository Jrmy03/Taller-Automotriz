namespace MotorSync.Domain.Entities;

/// <summary>Tabla PROVEEDOR_REPUESTO (muchos-a-muchos con dato propio: costo_adquisicion).</summary>
public class ProveedorRepuesto
{
    public int IdProveedor { get; set; }
    public int IdRepuesto { get; set; }
    public decimal CostoAdquisicion { get; set; }

    // Navegación
    public Proveedor Proveedor { get; set; } = null!;
    public Repuesto Repuesto { get; set; } = null!;
}
