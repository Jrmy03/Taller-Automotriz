using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla MECANICO.</summary>
public class Mecanico
{
    public int IdMecanico { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string Estado { get; set; } = EstadoRegistro.Activo;

    // Navegación (MECANICO_ESPECIALIDAD se mapea como muchos-a-muchos en Infrastructure)
    public ICollection<Especialidad> Especialidades { get; set; } = new List<Especialidad>();
    public ICollection<OrdenTrabajo> OrdenesTrabajo { get; set; } = new List<OrdenTrabajo>();

    public void Inactivar() => Estado = EstadoRegistro.Inactivo;
}
