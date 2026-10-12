using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Mecanicos;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Mecanicos.RegistrarMecanico;

public interface IRegistrarMecanicoUseCase
{
    Task<Result<MecanicoDto>> EjecutarAsync(RegistrarMecanicoDto dto, CancellationToken cancellationToken = default);
}

public sealed class RegistrarMecanicoUseCase(
    IMecanicoRepository mecanicoRepository,
    IUnitOfWork unitOfWork) : IRegistrarMecanicoUseCase
{
    public async Task<Result<MecanicoDto>> EjecutarAsync(RegistrarMecanicoDto dto, CancellationToken cancellationToken = default)
    {
        var nombre = dto.Nombre?.Trim() ?? string.Empty;
        if (nombre.Length == 0)
            return Result<MecanicoDto>.Failure("El nombre del mecánico es obligatorio.", ErrorType.Validation);

        var mecanico = new Mecanico
        {
            Nombre = nombre,
            Telefono = string.IsNullOrWhiteSpace(dto.Telefono) ? null : dto.Telefono.Trim(),
            Estado = EstadoRegistro.Activo
        };

        await mecanicoRepository.AddAsync(mecanico, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MecanicoDto>.Success(new MecanicoDto(
            mecanico.IdMecanico,
            mecanico.Nombre,
            mecanico.Telefono,
            mecanico.Estado,
            []));
    }
}
