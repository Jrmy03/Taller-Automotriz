using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla PROVEEDOR.</summary>
public class Proveedor
{
    public int IdProveedor { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public string? IdentificacionJuridica { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
    public string Estado { get; set; } = EstadoRegistro.Activo;

    // Navegación
    public ICollection<ProveedorRepuesto> ProveedorRepuestos { get; set; } = new List<ProveedorRepuesto>();

    public void Inactivar() => Estado = EstadoRegistro.Inactivo;
}
