using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Servicios;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Servicios.RegistrarServicio;

public interface IRegistrarServicioUseCase
{
    Task<Result<ServicioDto>> EjecutarAsync(RegistrarServicioDto dto, CancellationToken cancellationToken = default);
}

public sealed class RegistrarServicioUseCase(
    IServicioRepository servicioRepository,
    IUnitOfWork unitOfWork) : IRegistrarServicioUseCase
{
    public async Task<Result<ServicioDto>> EjecutarAsync(RegistrarServicioDto dto, CancellationToken cancellationToken = default)
    {
        var codigo = dto.Codigo?.Trim().ToUpperInvariant() ?? string.Empty;
        var nombre = dto.Nombre?.Trim() ?? string.Empty;

        if (codigo.Length == 0 || nombre.Length == 0)
            return Result<ServicioDto>.Failure("El código y nombre del servicio son obligatorios.", ErrorType.Validation);

        var existente = await servicioRepository.GetByCodigoAsync(codigo, cancellationToken);
        if (existente is not null)
            return Result<ServicioDto>.Failure($"Ya existe un servicio con el código '{codigo}'.", ErrorType.Conflict);

        var servicio = new Servicio
        {
            Codigo = codigo,
            Nombre = nombre,
            Descripcion = string.IsNullOrWhiteSpace(dto.Descripcion) ? null : dto.Descripcion.Trim(),
            CostoManoObra = dto.CostoManoObra,
            TiempoEstimado = dto.TiempoEstimado,
            Estado = EstadoRegistro.Activo
        };

        await servicioRepository.AddAsync(servicio, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ServicioDto>.Success(new ServicioDto(
            servicio.IdServicio,
            servicio.Codigo,
            servicio.Nombre,
            servicio.Descripcion,
            servicio.CostoManoObra,
            servicio.TiempoEstimado,
            servicio.Estado));
    }
}
