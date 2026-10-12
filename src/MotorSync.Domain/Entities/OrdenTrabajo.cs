using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla ORDEN_TRABAJO.</summary>
public class OrdenTrabajo
{
    public int IdOt { get; set; }
    public int IdVehiculo { get; set; }
    public int IdMecanico { get; set; }
    public int? IdCita { get; set; }
    public DateTime FechaApertura { get; set; }
    public DateTime? FechaCierre { get; set; }
    public int KilometrajeEntrada { get; set; }
    public string MotivoIngreso { get; set; } = string.Empty;
    public string Estado { get; set; } = EstadoOrdenTrabajo.Pendiente;

    // Navegación
    public Vehiculo Vehiculo { get; set; } = null!;
    public Mecanico Mecanico { get; set; } = null!;
    public Cita? Cita { get; set; }
    public ICollection<DetalleOt> Detalles { get; set; } = new List<DetalleOt>();
    public Factura? Factura { get; set; }

    // Comportamiento de dominio
    public decimal CalcularSubtotal() => Detalles.Sum(d => d.Subtotal);

    public void Cerrar(DateTime fechaCierre)
    {
        if (fechaCierre < FechaApertura)
            throw new InvalidOperationException("La fecha de cierre no puede ser anterior a la fecha de apertura.");

        FechaCierre = fechaCierre;
        Estado = EstadoOrdenTrabajo.Finalizada;
    }
}
