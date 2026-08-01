using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngresoVehiculoController : ControllerBase
{
    private readonly IIngresoVehiculoLN
        _ingresoVehiculoLN;

    public IngresoVehiculoController(
        IIngresoVehiculoLN ingresoVehiculoLN)
    {
        _ingresoVehiculoLN = ingresoVehiculoLN;
    }

    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<IngresoVehiculoDTO>>>>
        ObtenerTodos()
    {
        List<IngresoVehiculoDTO> ingresos =
            await _ingresoVehiculoLN
                .ObtenerTodosAsync();

        return Ok(
            Respuesta<List<IngresoVehiculoDTO>>
                .Exitosa(
                    ingresos,
                    "Ingresos consultados correctamente."
                )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<IngresoVehiculoDTO>>>
        ObtenerPorId(int id)
    {
        IngresoVehiculoDTO? ingreso =
            await _ingresoVehiculoLN
                .ObtenerPorIdAsync(id);

        if (ingreso is null)
        {
            return NotFound(
                Respuesta<IngresoVehiculoDTO>.Fallida(
                    "No se encontró el ingreso."
                )
            );
        }

        return Ok(
            Respuesta<IngresoVehiculoDTO>.Exitosa(
                ingreso,
                "Ingreso consultado correctamente."
            )
        );
    }

    [HttpPost]
    public async Task<ActionResult<
        Respuesta<IngresoVehiculoDTO>>>
        RegistrarIngreso(
            [FromBody] IngresoVehiculoDTO ingresoDTO)
    {
        try
        {
            IngresoVehiculoDTO creado =
                await _ingresoVehiculoLN
                    .RegistrarIngresoAsync(
                        ingresoDTO
                    );

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = creado.IngresoId },
                Respuesta<IngresoVehiculoDTO>.Exitosa(
                    creado,
                    "Ingreso registrado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<IngresoVehiculoDTO>.Fallida(
                    "No se pudo registrar el ingreso.",
                    [ex.Message]
                )
            );
        }
    }

    [HttpPut("{id:int}/salida")]
    public async Task<ActionResult<
        Respuesta<IngresoVehiculoDTO>>>
        RegistrarSalida(int id)
    {
        try
        {
            IngresoVehiculoDTO? ingreso =
                await _ingresoVehiculoLN
                    .RegistrarSalidaAsync(id);

            if (ingreso is null)
            {
                return NotFound(
                    Respuesta<IngresoVehiculoDTO>.Fallida(
                        "No se encontró el ingreso."
                    )
                );
            }

            return Ok(
                Respuesta<IngresoVehiculoDTO>.Exitosa(
                    ingreso,
                    "Salida registrada correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<IngresoVehiculoDTO>.Fallida(
                    "No se pudo registrar la salida.",
                    [ex.Message]
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                Respuesta<IngresoVehiculoDTO>.Fallida(
                    "La salida no es válida.",
                    [ex.Message]
                )
            );
        }
    }
}