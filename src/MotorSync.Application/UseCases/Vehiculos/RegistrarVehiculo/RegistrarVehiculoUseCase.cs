using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Vehiculos;
using MotorSync.Application.Mappings;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Vehiculos.RegistrarVehiculo;

public sealed class RegistrarVehiculoUseCase(
    IVehiculoRepository vehiculoRepository,
    IClienteRepository clienteRepository,
    IUnitOfWork unitOfWork) : IRegistrarVehiculoUseCase
{
    public async Task<Result<VehiculoDto>> EjecutarAsync(RegistrarVehiculoDto dto, CancellationToken cancellationToken = default)
    {
        var cliente = await clienteRepository.GetByIdAsync(dto.IdCliente, cancellationToken);
        if (cliente is null)
            return Result<VehiculoDto>.Failure($"No existe el cliente con id {dto.IdCliente}.", ErrorType.NotFound);

        if (!cliente.EstaActivo)
            return Result<VehiculoDto>.Failure("No se pueden registrar vehículos a un cliente inactivo.", ErrorType.Validation);

        var placa = dto.Placa.Trim().ToUpperInvariant();
        if (await vehiculoRepository.ExistsByPlacaAsync(placa, cancellationToken))
            return Result<VehiculoDto>.Failure($"Ya existe un vehículo con la placa '{placa}'.", ErrorType.Conflict);

        var vin = string.IsNullOrWhiteSpace(dto.Vin) ? null : dto.Vin.Trim().ToUpperInvariant();
        if (vin is not null && await vehiculoRepository.ExistsByVinAsync(vin, cancellationToken))
            return Result<VehiculoDto>.Failure($"Ya existe un vehículo con el VIN '{vin}'.", ErrorType.Conflict);

        var vehiculo = new Vehiculo
        {
            IdCliente = cliente.IdCliente,
            Placa = placa,
            Marca = dto.Marca.Trim(),
            Modelo = dto.Modelo.Trim(),
            Anio = dto.Anio,
            Vin = vin,
            Kilometraje = dto.Kilometraje,
            Estado = EstadoRegistro.Activo
        };

        await vehiculoRepository.AddAsync(vehiculo, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<VehiculoDto>.Success(vehiculo.ToDto());
    }
}
