using System.ComponentModel.DataAnnotations;

namespace MotorSync.Application.DTOs.OrdenesTrabajo;

public sealed class RegistrarDetalleOtDto
{
    public int? IdServicio { get; init; }
    public int? IdRepuesto { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Cantidad { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
    public decimal PrecioUnitario { get; init; }
}

public sealed class RegistrarOrdenTrabajoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un vehículo válido.")]
    public int IdVehiculo { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe asignar un mecánico válido.")]
    public int IdMecanico { get; init; }

    public int? IdCita { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El kilometraje de entrada no puede ser negativo.")]
    public int KilometrajeEntrada { get; init; }

    [Required, StringLength(250)]
    public string MotivoIngreso { get; init; } = string.Empty;

    public List<RegistrarDetalleOtDto> Detalles { get; init; } = [];
}
