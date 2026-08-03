using Microsoft.AspNetCore.Identity;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class AuthLN : IAuthLN
{
    private readonly IAuthAD _authAD;

    private readonly PasswordHasher<Usuario>
        _passwordHasher = new();

    public AuthLN(IAuthAD authAD)
    {
        _authAD = authAD;
    }

    public async Task CrearAdministradorInicialAsync(
        CrearAdministradorInicialDTO administradorDTO
    )
    {
        bool existeUsuario =
            await _authAD.ExisteUsuarioAsync();

        if (existeUsuario)
        {
            throw new InvalidOperationException(
                "El administrador inicial ya fue creado."
            );
        }

        Rol? rolAdministrador =
            await _authAD
                .ObtenerRolAdministradorAsync();

        if (rolAdministrador is null)
        {
            throw new InvalidOperationException(
                "No existe el rol Administrador."
            );
        }

        Usuario usuario = new()
        {
            RolId = rolAdministrador.RolId,

            Nombre =
                administradorDTO.Nombre.Trim(),

            Correo =
                administradorDTO.Correo
                    .Trim()
                    .ToLowerInvariant(),

            ClaveHash = string.Empty,

            Activo = true,

            FechaCreacion = DateTime.UtcNow
        };

        usuario.ClaveHash =
            _passwordHasher.HashPassword(
                usuario,
                administradorDTO.Clave
            );

        await _authAD.CrearUsuarioAsync(
            usuario
        );
    }

    public async Task<RespuestaLoginDTO>
        IniciarSesionAsync(
            LoginDTO loginDTO
        )
    {
        string correo =
            loginDTO.Correo
                .Trim()
                .ToLowerInvariant();

        Usuario? usuario =
            await _authAD
                .ObtenerUsuarioPorCorreoAsync(
                    correo
                );

        if (
            usuario is null ||
            !usuario.Activo ||
            usuario.Rol is null ||
            !usuario.Rol.Activo
        )
        {
            throw new UnauthorizedAccessException(
                "El correo o la contraseña son incorrectos."
            );
        }

        PasswordVerificationResult resultado =
            _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.ClaveHash,
                loginDTO.Clave
            );

        if (
            resultado ==
            PasswordVerificationResult.Failed
        )
        {
            throw new UnauthorizedAccessException(
                "El correo o la contraseña son incorrectos."
            );
        }

        return new RespuestaLoginDTO
        {
            UsuarioId = usuario.UsuarioId,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Rol = usuario.Rol.Nombre
        };
    }
}