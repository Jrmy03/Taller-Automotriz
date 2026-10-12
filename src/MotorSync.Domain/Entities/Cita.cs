using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla CITA.</summary>
public class Cita
{
    public int IdCita { get; set; }
    public int IdCliente { get; set; }
    public int IdVehiculo { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string? Motivo { get; set; }
    public string Estado { get; set; } = EstadoCita.Pendiente;

    // Navegación
    public Cliente Cliente { get; set; } = null!;
    public Vehiculo Vehiculo { get; set; } = null!;
    public OrdenTrabajo? OrdenTrabajo { get; set; }
}
