using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class IngresoVehiculoDTO
{
    public int IngresoId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Seleccione un vehículo válido."
    )]
    public int VehiculoId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Seleccione un parqueo válido."
    )]
    public int ParqueoId { get; set; }

    public int EspacioId { get; set; }

    public DateTime FechaIngreso { get; set; }

    public DateTime? FechaSalida { get; set; }

    public string Estado { get; set; } = string.Empty;
}