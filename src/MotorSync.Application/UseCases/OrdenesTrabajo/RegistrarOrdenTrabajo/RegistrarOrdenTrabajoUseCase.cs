using MotorSync.Application.Common;
using MotorSync.Application.DTOs.OrdenesTrabajo;
using MotorSync.Application.Interfaces.Services;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.OrdenesTrabajo.RegistrarOrdenTrabajo;

public interface IRegistrarOrdenTrabajoUseCase
{
    Task<Result<OrdenTrabajoDto>> EjecutarAsync(RegistrarOrdenTrabajoDto dto, CancellationToken cancellationToken = default);
}

public sealed class RegistrarOrdenTrabajoUseCase(
    IOrdenTrabajoRepository ordenTrabajoRepository,
    IVehiculoRepository vehiculoRepository,
    IMecanicoRepository mecanicoRepository,
    IServicioRepository servicioRepository,
    IRepuestoRepository repuestoRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IRegistrarOrdenTrabajoUseCase
{
    public async Task<Result<OrdenTrabajoDto>> EjecutarAsync(RegistrarOrdenTrabajoDto dto, CancellationToken cancellationToken = default)
    {
        var vehiculo = await vehiculoRepository.GetByIdAsync(dto.IdVehiculo, cancellationToken);
        if (vehiculo is null)
            return Result<OrdenTrabajoDto>.Failure($"No existe el vehículo con id {dto.IdVehiculo}.", ErrorType.NotFound);

        var mecanico = await mecanicoRepository.GetByIdAsync(dto.IdMecanico, cancellationToken);
        if (mecanico is null)
            return Result<OrdenTrabajoDto>.Failure($"No existe el mecánico con id {dto.IdMecanico}.", ErrorType.NotFound);

        var ot = new OrdenTrabajo
        {
            IdVehiculo = vehiculo.IdVehiculo,
            IdMecanico = mecanico.IdMecanico,
            IdCita = dto.IdCita,
            FechaApertura = dateTimeProvider.Now,
            KilometrajeEntrada = dto.KilometrajeEntrada,
            MotivoIngreso = dto.MotivoIngreso.Trim(),
            Estado = EstadoOrdenTrabajo.Pendiente
        };

        foreach (var item in dto.Detalles)
        {
            if (item.IdServicio.HasValue && item.IdRepuesto.HasValue)
                return Result<OrdenTrabajoDto>.Failure("Un detalle debe apuntar a un servicio o a un repuesto, no a ambos.", ErrorType.Validation);

            if (!item.IdServicio.HasValue && !item.IdRepuesto.HasValue)
                return Result<OrdenTrabajoDto>.Failure("Un detalle debe tener un servicio o un repuesto asignado.", ErrorType.Validation);

            if (item.IdServicio.HasValue)
            {
                var servicio = await servicioRepository.GetByIdAsync(item.IdServicio.Value, cancellationToken);
                if (servicio is null)
                    return Result<OrdenTrabajoDto>.Failure($"No existe el servicio con id {item.IdServicio.Value}.", ErrorType.NotFound);

                ot.Detalles.Add(DetalleOt.CrearDeServicio(servicio.IdServicio, item.Cantidad, item.PrecioUnitario));
            }
            else if (item.IdRepuesto.HasValue)
            {
                var repuesto = await repuestoRepository.GetByIdAsync(item.IdRepuesto.Value, cancellationToken);
                if (repuesto is null)
                    return Result<OrdenTrabajoDto>.Failure($"No existe el repuesto con id {item.IdRepuesto.Value}.", ErrorType.NotFound);

                repuesto.DescontarStock(item.Cantidad);
                ot.Detalles.Add(DetalleOt.CrearDeRepuesto(repuesto.IdRepuesto, item.Cantidad, item.PrecioUnitario));
            }
        }

        await ordenTrabajoRepository.AddAsync(ot, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var detallesDto = ot.Detalles.Select(d => new DetalleOtDto(
            d.IdDetalle,
            d.IdServicio,
            d.Servicio?.Nombre,
            d.IdRepuesto,
            d.Repuesto?.Nombre,
            d.Tipo,
            d.Cantidad,
            d.PrecioUnitario,
            d.Subtotal)).ToList();

        return Result<OrdenTrabajoDto>.Success(new OrdenTrabajoDto(
            ot.IdOt,
            ot.IdVehiculo,
            vehiculo.Placa,
            ot.IdMecanico,
            mecanico.Nombre,
            ot.IdCita,
            ot.FechaApertura,
            ot.FechaCierre,
            ot.KilometrajeEntrada,
            ot.MotivoIngreso,
            ot.Estado,
            ot.CalcularSubtotal(),
            detallesDto));
    }
}
