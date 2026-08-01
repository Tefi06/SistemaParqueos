using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarifaController : ControllerBase
{
    private readonly ITarifaLN _tarifaLN;

    public TarifaController(ITarifaLN tarifaLN)
    {
        _tarifaLN = tarifaLN;
    }

    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<TarifaDTO>>>> ObtenerTodos()
    {
        List<TarifaDTO> tarifas =
            await _tarifaLN.ObtenerTodosAsync();

        return Ok(
            Respuesta<List<TarifaDTO>>.Exitosa(
                tarifas,
                "Tarifas consultadas correctamente."
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<TarifaDTO>>> ObtenerPorId(int id)
    {
        TarifaDTO? tarifa =
            await _tarifaLN.ObtenerPorIdAsync(id);

        if (tarifa is null)
        {
            return NotFound(
                Respuesta<TarifaDTO>.Fallida(
                    "No se encontró la tarifa."
                )
            );
        }

        return Ok(
            Respuesta<TarifaDTO>.Exitosa(
                tarifa,
                "Tarifa consultada correctamente."
            )
        );
    }

    [HttpPost]
    public async Task<ActionResult<
        Respuesta<TarifaDTO>>> Crear(
        [FromBody] TarifaDTO tarifaDTO)
    {
        try
        {
            TarifaDTO creada =
                await _tarifaLN.CrearAsync(tarifaDTO);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = creada.TarifaId },
                Respuesta<TarifaDTO>.Exitosa(
                    creada,
                    "Tarifa creada correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<TarifaDTO>.Fallida(
                    "No se pudo crear la tarifa.",
                    [ex.Message]
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                Respuesta<TarifaDTO>.Fallida(
                    "Los datos de la tarifa no son válidos.",
                    [ex.Message]
                )
            );
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] TarifaDTO tarifaDTO)
    {
        try
        {
            bool actualizado =
                await _tarifaLN.ActualizarAsync(
                    id,
                    tarifaDTO
                );

            if (!actualizado)
            {
                return NotFound(
                    Respuesta<object?>.Fallida(
                        "No se encontró la tarifa."
                    )
                );
            }

            return Ok(
                Respuesta<object?>.Exitosa(
                    null,
                    "Tarifa actualizada correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<object?>.Fallida(
                    "No se pudo actualizar la tarifa.",
                    [ex.Message]
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                Respuesta<object?>.Fallida(
                    "Los datos de la tarifa no son válidos.",
                    [ex.Message]
                )
            );
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool eliminado =
            await _tarifaLN.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró la tarifa."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Tarifa desactivada correctamente."
            )
        );
    }
}