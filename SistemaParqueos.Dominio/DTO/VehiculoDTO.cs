using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class VehiculoDTO
{
    public int VehiculoId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Seleccione un cliente válido."
    )]
    public int ClienteId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Seleccione un tipo de vehículo válido."
    )]
    public int TipoVehiculoId { get; set; }

    [Required(ErrorMessage = "La placa es obligatoria.")]
    public string Placa { get; set; } = string.Empty;

    [Required(ErrorMessage = "La marca es obligatoria.")]
    public string Marca { get; set; } = string.Empty;

    public string? Modelo { get; set; }

    public string? Color { get; set; }

    public bool Activo { get; set; } = true;
}
