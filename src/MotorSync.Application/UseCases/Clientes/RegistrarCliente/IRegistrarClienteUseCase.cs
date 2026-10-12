using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Clientes;

namespace MotorSync.Application.UseCases.Clientes.RegistrarCliente;

public interface IRegistrarClienteUseCase
{
    Task<Result<ClienteDto>> EjecutarAsync(RegistrarClienteDto dto, CancellationToken cancellationToken = default);
}
