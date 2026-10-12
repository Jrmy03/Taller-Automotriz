using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla SERVICIO.</summary>
public class Servicio
{
    public int IdServicio { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal CostoManoObra { get; set; }

    /// <summary>Tiempo estimado (en minutos).</summary>
    public int? TiempoEstimado { get; set; }

    public string Estado { get; set; } = EstadoRegistro.Activo;

    // Navegación
    public ICollection<DetalleOt> DetallesOt { get; set; } = new List<DetalleOt>();

    public void Inactivar() => Estado = EstadoRegistro.Inactivo;
}
