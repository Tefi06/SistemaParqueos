using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class ClienteDTO
{
    public int ClienteId { get; set; }

    [Required(ErrorMessage = " Nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Apellidos son obligatorios.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cédula es obligatoria.")]
    public string Cedula { get; set; } = string.Empty;

    [Phone(ErrorMessage = "El número de teléfono no es válido.")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string? Correo { get; set; }

    public bool Activo { get; set; } = true;
}
