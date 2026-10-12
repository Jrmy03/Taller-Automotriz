using MotorSync.Application.DTOs.Clientes;

namespace MotorSync.Application.UseCases.Clientes.ConsultarClientes;

public interface IConsultarClientesUseCase
{
    Task<IReadOnlyList<ClienteDto>> EjecutarAsync(bool soloActivos, CancellationToken cancellationToken = default);
}
