using MotorSync.Domain.Constants;

namespace MotorSync.Domain.Entities;

/// <summary>Tabla CLIENTE.</summary>
public class Cliente
{
    public int IdCliente { get; set; }
    public string Identificacion { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string Estado { get; set; } = EstadoRegistro.Activo;

    // Navegación
    public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();

    // Comportamiento de dominio
    public bool EstaActivo => Estado == EstadoRegistro.Activo;

    public void ActualizarContacto(string? telefono, string? correo)
    {
        Telefono = telefono;
        Correo = correo;
    }

    public void Inactivar() => Estado = EstadoRegistro.Inactivo;

    public void Activar() => Estado = EstadoRegistro.Activo;
}
