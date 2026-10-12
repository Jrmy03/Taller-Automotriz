using Microsoft.AspNetCore.Mvc;
using MotorSync.Application.Common;
using MotorSync.Application.DTOs.Clientes;
using MotorSync.Application.UseCases.Clientes.BuscarClientePorIdentificacion;
using MotorSync.Application.UseCases.Clientes.ConsultarClientes;
using MotorSync.Application.UseCases.Clientes.RegistrarCliente;

namespace MotorSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClientesController(
    IConsultarClientesUseCase consultarClientesUseCase,
    IBuscarClientePorIdentificacionUseCase buscarClientePorIdentificacionUseCase,
    IRegistrarClienteUseCase registrarClienteUseCase) : ControllerBase
{
    /// <summary>
    /// Consulta el listado de clientes registrados en el taller.
    /// </summary>
    /// <param name="soloActivos">Indica si sólo se deben retornar clientes con estado 'Activo'.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClienteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClienteDto>>> ObtenerTodos(
        [FromQuery] bool soloActivos = false,
        CancellationToken cancellationToken = default)
    {
        var clientes = await consultarClientesUseCase.EjecutarAsync(soloActivos, cancellationToken);
        return Ok(clientes);
    }

    /// <summary>
    /// Busca un cliente por su número de identificación.
    /// </summary>
    /// <param name="identificacion">Cédula o identificación del cliente.</param>
    [HttpGet("{identificacion}")]
    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> ObtenerPorIdentificacion(
        string identificacion,
        CancellationToken cancellationToken = default)
    {
        var resultado = await buscarClientePorIdentificacionUseCase.EjecutarAsync(identificacion, cancellationToken);

        if (!resultado.IsSuccess)
        {
            return resultado.ErrorType switch
            {
                ErrorType.NotFound => NotFound(new { error = resultado.Error }),
                _ => BadRequest(new { error = resultado.Error })
            };
        }

        return Ok(resultado.Value);
    }

    /// <summary>
    /// Registra un nuevo cliente en el sistema MotorSync.
    /// </summary>
    /// <param name="dto">Datos del cliente a registrar.</param>
    [HttpPost]
    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClienteDto>> Registrar(
        [FromBody] RegistrarClienteDto dto,
        CancellationToken cancellationToken = default)
    {
        var resultado = await registrarClienteUseCase.EjecutarAsync(dto, cancellationToken);

        if (!resultado.IsSuccess)
        {
            return resultado.ErrorType switch
            {
                ErrorType.Conflict => Conflict(new { error = resultado.Error }),
                _ => BadRequest(new { error = resultado.Error })
            };
        }

        return CreatedAtAction(
            nameof(ObtenerPorIdentificacion),
            new { identificacion = resultado.Value!.Identificacion },
            resultado.Value);
    }
}
