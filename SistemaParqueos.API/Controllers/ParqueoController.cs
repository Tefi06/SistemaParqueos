using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParqueoController : ControllerBase
{
    private readonly IParqueoLN _parqueoLN;

    public ParqueoController(IParqueoLN parqueoLN)
    {
        _parqueoLN = parqueoLN;
    }

    
    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<ParqueoDTO>>>> ObtenerTodos()
    {
        List<ParqueoDTO> parqueos =
            await _parqueoLN.ObtenerTodosAsync();

        return Ok(
            Respuesta<List<ParqueoDTO>>.Exitosa(
                parqueos,
                "Parqueos consultados correctamente."
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<ParqueoDTO>>> ObtenerPorId(int id)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<ParqueoDTO>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        ParqueoDTO? parqueo =
            await _parqueoLN.ObtenerPorIdAsync(id);

        if (parqueo is null)
        {
            return NotFound(
                Respuesta<ParqueoDTO>.Fallida(
                    "No se encontró el parqueo."
                )
            );
        }

        return Ok(
            Respuesta<ParqueoDTO>.Exitosa(
                parqueo,
                "Parqueo consultado correctamente."
            )
        );
    }

   
    [HttpPost]
    public async Task<ActionResult<
        Respuesta<ParqueoDTO>>> Crear(
        [FromBody] ParqueoDTO parqueoDTO)
    {
        ParqueoDTO creado =
            await _parqueoLN.CrearAsync(parqueoDTO);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new
            {
                id = creado.ParqueoId
            },
            Respuesta<ParqueoDTO>.Exitosa(
                creado,
                "Parqueo creado correctamente."
            )
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ParqueoDTO parqueoDTO)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<object?>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        bool actualizado =
            await _parqueoLN.ActualizarAsync(
                id,
                parqueoDTO
            );

        if (!actualizado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el parqueo."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Parqueo actualizado correctamente."
            )
        );
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<object?>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        bool eliminado =
            await _parqueoLN.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el parqueo."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Parqueo desactivado correctamente."
            )
        );
    }
}