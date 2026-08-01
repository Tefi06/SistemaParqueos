using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiculoController : ControllerBase
{
    private readonly IVehiculoLN _vehiculoLN;

    public VehiculoController(IVehiculoLN vehiculoLN)
    {
        _vehiculoLN = vehiculoLN;
    }

   
    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<VehiculoDTO>>>> ObtenerTodos()
    {
        List<VehiculoDTO> vehiculos =
            await _vehiculoLN.ObtenerTodosAsync();

        return Ok(
            Respuesta<List<VehiculoDTO>>.Exitosa(
                vehiculos,
                "Vehículos consultados correctamente."
            )
        );
    }

   
    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<VehiculoDTO>>> ObtenerPorId(int id)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<VehiculoDTO>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        VehiculoDTO? vehiculo =
            await _vehiculoLN.ObtenerPorIdAsync(id);

        if (vehiculo is null)
        {
            return NotFound(
                Respuesta<VehiculoDTO>.Fallida(
                    "No se encontró el vehículo."
                )
            );
        }

        return Ok(
            Respuesta<VehiculoDTO>.Exitosa(
                vehiculo,
                "Vehículo consultado correctamente."
            )
        );
    }

    
    [HttpPost]
    public async Task<ActionResult<
        Respuesta<VehiculoDTO>>> Crear(
        [FromBody] VehiculoDTO vehiculoDTO)
    {
        try
        {
            VehiculoDTO creado =
                await _vehiculoLN.CrearAsync(vehiculoDTO);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new
                {
                    id = creado.VehiculoId
                },
                Respuesta<VehiculoDTO>.Exitosa(
                    creado,
                    "Vehículo creado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<VehiculoDTO>.Fallida(
                    "No se pudo crear el vehículo.",
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
        [FromBody] VehiculoDTO vehiculoDTO)
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
                await _vehiculoLN.ActualizarAsync(
                    id,
                    vehiculoDTO
                );

            if (!actualizado)
            {
                return NotFound(
                    Respuesta<object?>.Fallida(
                        "No se encontró el vehículo."
                    )
                );
            }

            return Ok(
                Respuesta<object?>.Exitosa(
                    null,
                    "Vehículo actualizado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<object?>.Fallida(
                    "No se pudo actualizar el vehículo.",
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
            await _vehiculoLN.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el vehículo."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Vehículo desactivado correctamente."
            )
        );
    }
}
