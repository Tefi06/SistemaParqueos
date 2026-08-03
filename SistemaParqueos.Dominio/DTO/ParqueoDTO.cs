using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class ParqueoDTO
{
    public int ParqueoId { get; set; }

    [Required(ErrorMessage = "El nombre del parqueo es obligatorio.")]
    public string NombreParqueo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string Direccion { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Numero de teléfono no válido.")]
    public string? Telefono { get; set; }


    //No permite numeros negativos ni 0 de capacidad
    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "La capacidad total debe ser mayor que cero."
    )]
    public int CapacidadTotal { get; set; }

    public int EspaciosDisponibles { get; set; }


    public int EspaciosOcupados { get; set; }

    public bool Activo { get; set; } = true;

}