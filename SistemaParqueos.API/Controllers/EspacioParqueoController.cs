using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EspacioParqueoController : ControllerBase
{
    private readonly IEspacioParqueoLN _espacioParqueoLN;

    public EspacioParqueoController(
        IEspacioParqueoLN espacioParqueoLN)
    {
        _espacioParqueoLN = espacioParqueoLN;
    }

    
    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<EspacioParqueoDTO>>>> ObtenerTodos()
    {
        List<EspacioParqueoDTO> espacios =
            await _espacioParqueoLN.ObtenerTodosAsync();

        return Ok(
            Respuesta<List<EspacioParqueoDTO>>.Exitosa(
                espacios,
                "Espacios de parqueo consultados correctamente."
            )
        );
    }

   
    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<EspacioParqueoDTO>>> ObtenerPorId(int id)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<EspacioParqueoDTO>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        EspacioParqueoDTO? espacio =
            await _espacioParqueoLN.ObtenerPorIdAsync(id);

        if (espacio is null)
        {
            return NotFound(
                Respuesta<EspacioParqueoDTO>.Fallida(
                    "No se encontró el espacio de parqueo."
                )
            );
        }

        return Ok(
            Respuesta<EspacioParqueoDTO>.Exitosa(
                espacio,
                "Espacio de parqueo consultado correctamente."
            )
        );
    }

    [HttpPost]
    public async Task<ActionResult<
        Respuesta<EspacioParqueoDTO>>> Crear(
        [FromBody] EspacioParqueoDTO espacioParqueoDTO)
    {
        try
        {
            EspacioParqueoDTO creado =
                await _espacioParqueoLN.CrearAsync(
                    espacioParqueoDTO
                );

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new
                {
                    id = creado.EspacioId
                },
                Respuesta<EspacioParqueoDTO>.Exitosa(
                    creado,
                    "Espacio de parqueo creado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<EspacioParqueoDTO>.Fallida(
                    "No se pudo crear el espacio de parqueo.",
                    new List<string>
                    {
                        ex.Message
                    }
                )
            );
        }
    }

   
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] EspacioParqueoDTO espacioParqueoDTO)
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

        try
        {
            bool actualizado =
                await _espacioParqueoLN.ActualizarAsync(
                    id,
                    espacioParqueoDTO
                );

            if (!actualizado)
            {
                return NotFound(
                    Respuesta<object?>.Fallida(
                        "No se encontró el espacio de parqueo."
                    )
                );
            }

            return Ok(
                Respuesta<object?>.Exitosa(
                    null,
                    "Espacio de parqueo actualizado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<object?>.Fallida(
                    "No se pudo actualizar el espacio de parqueo.",
                    new List<string>
                    {
                        ex.Message
                    }
                )
            );
        }
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
            await _espacioParqueoLN.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el espacio de parqueo."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Espacio de parqueo desactivado correctamente."
            )
        );
    }
}