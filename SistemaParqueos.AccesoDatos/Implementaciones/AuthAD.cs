using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class AuthAD : IAuthAD
{
    private readonly ParqueosContext _context;

    public AuthAD(ParqueosContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteUsuarioAsync()
    {
        return await _context.Usuarios.AnyAsync();
    }

    public async Task<Usuario?> ObtenerUsuarioPorCorreoAsync(
        string correo
    )
    {
        return await _context.Usuarios
            .Include(usuario => usuario.Rol)
            .FirstOrDefaultAsync(
                usuario => usuario.Correo == correo
            );
    }

    public async Task<Rol?> ObtenerRolAdministradorAsync()
    {
        return await _context.Roles
            .FirstOrDefaultAsync(
                rol =>
                    rol.Nombre == "Administrador" &&
                    rol.Activo
            );
    }

    public async Task<Usuario> CrearUsuarioAsync(
        Usuario usuario
    )
    {
        await _context.Usuarios.AddAsync(usuario);

        await _context.SaveChangesAsync();

        return usuario;
    }
}
