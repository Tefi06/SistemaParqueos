using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthLN _authLN;

    public AuthController(IAuthLN authLN)
    {
        _authLN = authLN;
    }

    [HttpPost("CrearAdministradorInicial")]
    public async Task<IActionResult>
        CrearAdministradorInicial(
            CrearAdministradorInicialDTO
                administradorDTO
        )
    {
        try
        {
            await _authLN
                .CrearAdministradorInicialAsync(
                    administradorDTO
                );

            return Ok(new
            {
                exito = true,

                mensaje =
                    "El administrador inicial fue creado correctamente.",

                datos = (object?)null,

                errores =
                    Array.Empty<string>()
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                exito = false,

                mensaje =
                    "No fue posible crear el administrador.",

                datos = (object?)null,

                errores =
                    new[]
                    {
                        ex.Message
                    }
            });
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(
        LoginDTO loginDTO
    )
    {
        try
        {
            RespuestaLoginDTO resultado =
                await _authLN
                    .IniciarSesionAsync(
                        loginDTO
                    );

            return Ok(new
            {
                exito = true,

                mensaje =
                    "Inicio de sesión correcto.",

                datos = resultado,

                errores =
                    Array.Empty<string>()
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new
            {
                exito = false,

                mensaje =
                    "No fue posible iniciar sesión.",

                datos = (object?)null,

                errores =
                    new[]
                    {
                        ex.Message
                    }
            });
        }
    }
}