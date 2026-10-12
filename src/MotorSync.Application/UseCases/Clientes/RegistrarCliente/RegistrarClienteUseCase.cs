using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Clientes;
using MotorSync.Application.Mappings;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Clientes.RegistrarCliente;

public sealed class RegistrarClienteUseCase(
    IClienteRepository clienteRepository,
    IUnitOfWork unitOfWork) : IRegistrarClienteUseCase
{
    public async Task<Result<ClienteDto>> EjecutarAsync(RegistrarClienteDto dto, CancellationToken cancellationToken = default)
    {
        var identificacion = dto.Identificacion?.Trim() ?? string.Empty;
        var nombre = dto.NombreCompleto?.Trim() ?? string.Empty;

        if (identificacion.Length == 0 || nombre.Length == 0)
            return Result<ClienteDto>.Failure("La identificación y el nombre completo son obligatorios.", ErrorType.Validation);

        // Regla de negocio: la identificación es única (UNIQUE en CLIENTE.identificacion)
        if (await clienteRepository.ExistsByIdentificacionAsync(identificacion, cancellationToken))
            return Result<ClienteDto>.Failure($"Ya existe un cliente con la identificación '{identificacion}'.", ErrorType.Conflict);

        var cliente = new Cliente
        {
            Identificacion = identificacion,
            NombreCompleto = nombre,
            Telefono = string.IsNullOrWhiteSpace(dto.Telefono) ? null : dto.Telefono.Trim(),
            Correo = string.IsNullOrWhiteSpace(dto.Correo) ? null : dto.Correo.Trim(),
            Estado = EstadoRegistro.Activo
        };

        await clienteRepository.AddAsync(cliente, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ClienteDto>.Success(cliente.ToDto());
    }
}
