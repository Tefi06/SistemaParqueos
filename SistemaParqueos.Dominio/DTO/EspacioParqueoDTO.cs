using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class EspacioParqueoDTO
{
    public int EspacioId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Seleccione un parqueo válido."
    )]
    public int ParqueoId { get; set; }

    [Required(ErrorMessage = "El número del espacio es obligatorio.")]
    public string NumeroEspacio { get; set; } = string.Empty;

    public bool Disponible { get; set; } = true;

    public bool Activo { get; set; } = true;
}