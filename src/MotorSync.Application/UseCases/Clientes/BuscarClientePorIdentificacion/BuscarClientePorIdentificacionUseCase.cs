using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Clientes;
using MotorSync.Application.Mappings;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Clientes.BuscarClientePorIdentificacion;

public sealed class BuscarClientePorIdentificacionUseCase(IClienteRepository clienteRepository)
    : IBuscarClientePorIdentificacionUseCase
{
    public async Task<Result<ClienteDto>> EjecutarAsync(string identificacion, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(identificacion))
            return Result<ClienteDto>.Failure("Debe indicar una identificación.", ErrorType.Validation);

        var cliente = await clienteRepository.GetByIdentificacionAsync(identificacion.Trim(), cancellationToken);

        return cliente is null
            ? Result<ClienteDto>.Failure($"No existe un cliente con la identificación '{identificacion}'.", ErrorType.NotFound)
            : Result<ClienteDto>.Success(cliente.ToDto());
    }
}
