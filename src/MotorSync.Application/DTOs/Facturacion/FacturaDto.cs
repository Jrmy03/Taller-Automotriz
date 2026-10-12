namespace MotorSync.Application.DTOs.Facturacion;

public sealed record PagoDto(
    int IdPago,
    int IdFactura,
    DateTime FechaPago,
    decimal Monto,
    string MetodoPago,
    string Estado);

public sealed record FacturaDto(
    int IdFactura,
    int IdOt,
    string NumeroFactura,
    DateTime Fecha,
    decimal Subtotal,
    decimal Impuesto,
    decimal Total,
    decimal TotalPagado,
    decimal SaldoPendiente,
    string Estado,
    IReadOnlyList<PagoDto> Pagos);
