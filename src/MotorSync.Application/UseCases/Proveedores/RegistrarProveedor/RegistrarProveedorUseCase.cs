using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Proveedores;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;
using MotorSync.Domain.Interfaces.Repositories;

namespace MotorSync.Application.UseCases.Proveedores.RegistrarProveedor;

public interface IRegistrarProveedorUseCase
{
    Task<Result<ProveedorDto>> EjecutarAsync(RegistrarProveedorDto dto, CancellationToken cancellationToken = default);
}

public sealed class RegistrarProveedorUseCase(
    IProveedorRepository proveedorRepository,
    IUnitOfWork unitOfWork) : IRegistrarProveedorUseCase
{
    public async Task<Result<ProveedorDto>> EjecutarAsync(RegistrarProveedorDto dto, CancellationToken cancellationToken = default)
    {
        var razonSocial = dto.RazonSocial?.Trim() ?? string.Empty;
        if (razonSocial.Length == 0)
            return Result<ProveedorDto>.Failure("La razón social es obligatoria.", ErrorType.Validation);

        var idJuridica = string.IsNullOrWhiteSpace(dto.IdentificacionJuridica)
            ? null
            : dto.IdentificacionJuridica.Trim();

        if (idJuridica is not null)
        {
            var existente = await proveedorRepository.GetByIdentificacionJuridicaAsync(idJuridica, cancellationToken);
            if (existente is not null)
                return Result<ProveedorDto>.Failure($"Ya existe un proveedor con la cédula jurídica '{idJuridica}'.", ErrorType.Conflict);
        }

        var proveedor = new Proveedor
        {
            RazonSocial = razonSocial,
            IdentificacionJuridica = idJuridica,
            Telefono = string.IsNullOrWhiteSpace(dto.Telefono) ? null : dto.Telefono.Trim(),
            Correo = string.IsNullOrWhiteSpace(dto.Correo) ? null : dto.Correo.Trim(),
            Direccion = string.IsNullOrWhiteSpace(dto.Direccion) ? null : dto.Direccion.Trim(),
            Estado = EstadoRegistro.Activo
        };

        await proveedorRepository.AddAsync(proveedor, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ProveedorDto>.Success(new ProveedorDto(
            proveedor.IdProveedor,
            proveedor.RazonSocial,
            proveedor.IdentificacionJuridica,
            proveedor.Telefono,
            proveedor.Correo,
            proveedor.Direccion,
            proveedor.Estado));
    }
}
