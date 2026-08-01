using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class TarifaDTO
{
    public int TarifaId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Seleccione un tipo de vehículo válido."
    )]
    public int TipoVehiculoId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(
       typeof(decimal),
       "0.01",
       "999999999999999999",
       ParseLimitsInInvariantCulture = true,
       ErrorMessage = "El monto por hora debe ser mayor que cero."
   )]
    public decimal MontoHora { get; set; }

    public bool Activo { get; set; } = true;
}
