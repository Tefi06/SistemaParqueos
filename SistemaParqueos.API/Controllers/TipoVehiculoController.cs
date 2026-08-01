using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoVehiculoController : ControllerBase
{
    private readonly ITipoVehiculoLN _tipoVehiculoLN;

    public TipoVehiculoController(
        ITipoVehiculoLN tipoVehiculoLN)
    {
        _tipoVehiculoLN = tipoVehiculoLN;
    }

   
    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<TipoVehiculoDTO>>>> ObtenerTodos()
    {
        List<TipoVehiculoDTO> tiposVehiculo =
            await _tipoVehiculoLN.ObtenerTodosAsync();

        Respuesta<List<TipoVehiculoDTO>> respuesta =
            Respuesta<List<TipoVehiculoDTO>>.Exitosa(
                tiposVehiculo,
                "Tipos de vehículo consultados correctamente."
            );

        return Ok(respuesta);
    }

    //Obtener el tipo de vehiculo por id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<TipoVehiculoDTO>>> ObtenerPorId(int id)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<TipoVehiculoDTO>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        TipoVehiculoDTO? tipoVehiculo =
            await _tipoVehiculoLN.ObtenerPorIdAsync(id);

        if (tipoVehiculo is null)
        {
            return NotFound(
                Respuesta<TipoVehiculoDTO>.Fallida(
                    "No se encontró el tipo de vehículo."
                )
            );
        }

        return Ok(
            Respuesta<TipoVehiculoDTO>.Exitosa(
                tipoVehiculo,
                "Tipo de vehículo consultado correctamente."
            )
        );
    }

    // Añadir tipo de vehiculo
    // POST: api/TipoVehiculo
    [HttpPost]
    public async Task<ActionResult<
        Respuesta<TipoVehiculoDTO>>> Crear(
        [FromBody] TipoVehiculoCrearDTO tipoVehiculoDTO)
    {
        try
        {
            TipoVehiculoDTO creado =
                await _tipoVehiculoLN.CrearAsync(tipoVehiculoDTO);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = creado.TipoVehiculoId },
                Respuesta<TipoVehiculoDTO>.Exitosa(
                    creado,
                    "Tipo de vehículo creado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<TipoVehiculoDTO>.Fallida(
                    "No se pudo crear el tipo de vehículo.",
                    new List<string>
                    {
                    ex.Message
                    }
                )
            );
        }
    }


    // [HttpPut("{id:int}")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
    int id,
    [FromBody] TipoVehiculoActualizarDTO tipoVehiculoDTO)
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
            await _tipoVehiculoLN.ActualizarAsync(
                id,
                tipoVehiculoDTO
            );

        if (!actualizado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el tipo de vehículo."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Tipo de vehículo actualizado correctamente."
            )
        );
    }

    //Borramos el tipo de vehiculo
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
            await _tipoVehiculoLN.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el tipo de vehículo."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Tipo de vehículo desactivado correctamente."
            )
        );
    }
}
