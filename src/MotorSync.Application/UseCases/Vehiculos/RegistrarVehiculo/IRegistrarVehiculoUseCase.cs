using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Vehiculos;

namespace MotorSync.Application.UseCases.Vehiculos.RegistrarVehiculo;

public interface IRegistrarVehiculoUseCase
{
    Task<Result<VehiculoDto>> EjecutarAsync(RegistrarVehiculoDto dto, CancellationToken cancellationToken = default);
}
