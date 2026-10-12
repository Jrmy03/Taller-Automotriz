using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Clientes;

namespace MotorSync.Application.UseCases.Clientes.BuscarClientePorIdentificacion;

public interface IBuscarClientePorIdentificacionUseCase
{
    Task<Result<ClienteDto>> EjecutarAsync(string identificacion, CancellationToken cancellationToken = default);
}
