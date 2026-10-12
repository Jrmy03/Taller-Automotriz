namespace MotorSync.Application.DTOs.OrdenesTrabajo;

public sealed record DetalleOtDto(
    int IdDetalle,
    int? IdServicio,
    string? DescripcionServicio,
    int? IdRepuesto,
    string? DescripcionRepuesto,
    string Tipo,
    int Cantidad,
    decimal PrecioUnitario,
    decimal Subtotal);

public sealed record OrdenTrabajoDto(
    int IdOt,
    int IdVehiculo,
    string PlacaVehiculo,
    int IdMecanico,
    string NombreMecanico,
    int? IdCita,
    DateTime FechaApertura,
    DateTime? FechaCierre,
    int KilometrajeEntrada,
    string MotivoIngreso,
    string Estado,
    decimal Subtotal,
    IReadOnlyList<DetalleOtDto> Detalles);
