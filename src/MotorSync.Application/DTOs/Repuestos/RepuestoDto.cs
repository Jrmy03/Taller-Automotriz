namespace MotorSync.Application.DTOs.Repuestos;

public sealed record RepuestoDto(
    int IdRepuesto,
    string? CodigoParte,
    string Nombre,
    string? Categoria,
    decimal CostoCompra,
    decimal PrecioVenta,
    int StockActual,
    int StockMinimo,
    string Estado,
    bool RequiereReabastecimiento);
