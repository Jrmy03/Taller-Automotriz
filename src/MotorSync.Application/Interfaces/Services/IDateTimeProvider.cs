namespace MotorSync.Application.Interfaces.Services;

/// <summary>
/// Abstracción del reloj del sistema. Permite probar casos de uso que dependen
/// de la fecha (apertura/cierre de OT, facturación) sin usar DateTime.Now directamente.
/// </summary>
public interface IDateTimeProvider
{
    DateTime Now { get; }
}
