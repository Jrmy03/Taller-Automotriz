namespace MotorSync.Domain.Entities;

/// <summary>Tabla ESPECIALIDAD.</summary>
public class Especialidad
{
    public int IdEspecialidad { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    // Navegación
    public ICollection<Mecanico> Mecanicos { get; set; } = new List<Mecanico>();
}
