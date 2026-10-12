using MotorSync.Application.DTOs.Clientes;
using MotorSync.Application.Mappings;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Clientes.ConsultarClientes;

public sealed class ConsultarClientesUseCase(IClienteRepository clienteRepository) : IConsultarClientesUseCase
{
    public async Task<IReadOnlyList<ClienteDto>> EjecutarAsync(bool soloActivos, CancellationToken cancellationToken = default)
    {
        var clientes = soloActivos
            ? await clienteRepository.GetActivosAsync(cancellationToken)
            : await clienteRepository.GetAllAsync(cancellationToken);

        return clientes
            .OrderBy(c => c.NombreCompleto)
            .Select(c => c.ToDto())
            .ToList();
    }
}
