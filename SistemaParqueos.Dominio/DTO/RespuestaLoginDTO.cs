namespace SistemaParqueos.Dominio.DTO;

public class RespuestaLoginDTO
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Rol { get; set; } = string.Empty;
}