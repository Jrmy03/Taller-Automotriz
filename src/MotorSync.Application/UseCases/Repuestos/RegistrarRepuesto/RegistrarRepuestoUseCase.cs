using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Repuestos;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Repuestos.RegistrarRepuesto;

public interface IRegistrarRepuestoUseCase
{
    Task<Result<RepuestoDto>> EjecutarAsync(RegistrarRepuestoDto dto, CancellationToken cancellationToken = default);
}

public sealed class RegistrarRepuestoUseCase(
    IRepuestoRepository repuestoRepository,
    IUnitOfWork unitOfWork) : IRegistrarRepuestoUseCase
{
    public async Task<Result<RepuestoDto>> EjecutarAsync(RegistrarRepuestoDto dto, CancellationToken cancellationToken = default)
    {
        var nombre = dto.Nombre?.Trim() ?? string.Empty;
        if (nombre.Length == 0)
            return Result<RepuestoDto>.Failure("El nombre del repuesto es obligatorio.", ErrorType.Validation);

        var codigoParte = string.IsNullOrWhiteSpace(dto.CodigoParte) ? null : dto.CodigoParte.Trim().ToUpperInvariant();
        if (codigoParte is not null)
        {
            var existente = await repuestoRepository.GetByCodigoParteAsync(codigoParte, cancellationToken);
            if (existente is not null)
                return Result<RepuestoDto>.Failure($"Ya existe un repuesto con el código de parte '{codigoParte}'.", ErrorType.Conflict);
        }

        var repuesto = new Repuesto
        {
            CodigoParte = codigoParte,
            Nombre = nombre,
            Categoria = string.IsNullOrWhiteSpace(dto.Categoria) ? null : dto.Categoria.Trim(),
            CostoCompra = dto.CostoCompra,
            PrecioVenta = dto.PrecioVenta,
            StockActual = dto.StockActual,
            StockMinimo = dto.StockMinimo,
            Estado = EstadoRegistro.Activo
        };

        await repuestoRepository.AddAsync(repuesto, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RepuestoDto>.Success(new RepuestoDto(
            repuesto.IdRepuesto,
            repuesto.CodigoParte,
            repuesto.Nombre,
            repuesto.Categoria,
            repuesto.CostoCompra,
            repuesto.PrecioVenta,
            repuesto.StockActual,
            repuesto.StockMinimo,
            repuesto.Estado,
            repuesto.RequiereReabastecimiento));
    }
}
