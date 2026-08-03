using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IAuthAD
{
    Task<bool> ExisteUsuarioAsync();

    Task<Usuario?> ObtenerUsuarioPorCorreoAsync(
        string correo
    );

    Task<Rol?> ObtenerRolAdministradorAsync();

    Task<Usuario> CrearUsuarioAsync(
        Usuario usuario
    );
}
