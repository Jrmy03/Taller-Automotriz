namespace MotorSync.Domain.Constants;

/// <summary>
/// Estados para catálogos (CLIENTE, VEHICULO, MECANICO, PROVEEDOR, SERVICIO, REPUESTO).
/// Coinciden con los valores VARCHAR de la base de datos TallerAutomotriz.
/// </summary>
public static class EstadoRegistro
{
    public const string Activo = "Activo";
    public const string Inactivo = "Inactivo";
}

public static class EstadoCita
{
    public const string Pendiente = "Pendiente";
    public const string Confirmada = "Confirmada";
    public const string Cancelada = "Cancelada";
    public const string Atendida = "Atendida";
}

public static class EstadoOrdenTrabajo
{
    public const string Pendiente = "Pendiente";
    public const string EnProceso = "En Proceso";
    public const string Finalizada = "Finalizada";
    public const string Cancelada = "Cancelada";
}

public static class EstadoFactura
{
    public const string Pendiente = "Pendiente";
    public const string Pagada = "Pagada";
    public const string Anulada = "Anulada";
}

public static class EstadoPago
{
    public const string Registrado = "Registrado";
    public const string Anulado = "Anulado";
}

/// <summary>Valores válidos para DETALLE_OT.tipo (CK_DETALLE_TIPO).</summary>
public static class TipoDetalleOt
{
    public const string Servicio = "Servicio";
    public const string Repuesto = "Repuesto";
}
