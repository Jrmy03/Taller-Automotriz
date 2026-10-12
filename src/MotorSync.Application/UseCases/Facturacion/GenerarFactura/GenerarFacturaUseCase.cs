using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Facturacion;
using MotorSync.Application.Interfaces.Services;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Facturacion.GenerarFactura;

public interface IGenerarFacturaUseCase
{
    Task<Result<FacturaDto>> EjecutarAsync(GenerarFacturaDto dto, CancellationToken cancellationToken = default);
}

public sealed class GenerarFacturaUseCase(
    IFacturaRepository facturaRepository,
    IOrdenTrabajoRepository ordenTrabajoRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IGenerarFacturaUseCase
{
    public async Task<Result<FacturaDto>> EjecutarAsync(GenerarFacturaDto dto, CancellationToken cancellationToken = default)
    {
        var ot = await ordenTrabajoRepository.GetConDetallesAsync(dto.IdOt, cancellationToken);
        if (ot is null)
            return Result<FacturaDto>.Failure($"No existe la orden de trabajo con id {dto.IdOt}.", ErrorType.NotFound);

        var facturaExistente = await facturaRepository.GetByOrdenTrabajoAsync(dto.IdOt, cancellationToken);
        if (facturaExistente is not null)
            return Result<FacturaDto>.Failure($"La orden de trabajo {dto.IdOt} ya tiene la factura '{facturaExistente.NumeroFactura}' asociada.", ErrorType.Conflict);

        var numeroExistente = await facturaRepository.GetByNumeroAsync(dto.NumeroFactura.Trim(), cancellationToken);
        if (numeroExistente is not null)
            return Result<FacturaDto>.Failure($"Ya existe una factura con el número '{dto.NumeroFactura}'.", ErrorType.Conflict);

        var subtotal = ot.CalcularSubtotal();
        var factura = new Factura
        {
            IdOt = ot.IdOt,
            NumeroFactura = dto.NumeroFactura.Trim(),
            Fecha = dateTimeProvider.Now,
            Estado = EstadoFactura.Pendiente
        };
        factura.CalcularTotales(subtotal, dto.PorcentajeImpuesto);

        await facturaRepository.AddAsync(factura, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<FacturaDto>.Success(new FacturaDto(
            factura.IdFactura,
            factura.IdOt,
            factura.NumeroFactura,
            factura.Fecha,
            factura.Subtotal,
            factura.Impuesto,
            factura.Total,
            factura.TotalPagado,
            factura.SaldoPendiente,
            factura.Estado,
            []));
    }
}
