using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class FacturaDTO
{
    public int FacturaId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Debe seleccionar un ingreso válido."
    )]
    public int IngresoId { get; set; }

    public DateTime FechaFactura { get; set; }

    [Range(
      typeof(decimal),
      "0",
      "999999999999999999",
      ParseLimitsInInvariantCulture = true,
      ErrorMessage = "Las horas cobradas no pueden ser negativas."
  )]
    public decimal HorasCobradas { get; set; }


    [Range(
        typeof(decimal),
        "0",
        "999999999999999999",
        ParseLimitsInInvariantCulture = true,
        ErrorMessage = "El monto total no puede ser negativo."
    )]
    public decimal MontoTotal { get; set; }

    public string? Placa { get; set; }

    public string? Vehiculo { get; set; }

    public string? Cliente { get; set; }
}
