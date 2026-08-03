using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    [StringLength(
        150,
        ErrorMessage = "El correo no puede superar los 150 caracteres."
    )]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(
        100,
        MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres."
    )]
    public string Clave { get; set; } = string.Empty;
}