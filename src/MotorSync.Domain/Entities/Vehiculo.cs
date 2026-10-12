using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla VEHICULO.</summary>
public class Vehiculo
{
    public int IdVehiculo { get; set; }
    public int IdCliente { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public short? Anio { get; set; }
    public string? Vin { get; set; }
    public int Kilometraje { get; set; }
    public string Estado { get; set; } = EstadoRegistro.Activo;

    // Navegación
    public Cliente Cliente { get; set; } = null!;
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ICollection<OrdenTrabajo> OrdenesTrabajo { get; set; } = new List<OrdenTrabajo>();

    public void Inactivar() => Estado = EstadoRegistro.Inactivo;
}
