using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacturaController : ControllerBase
{
    private readonly IFacturaLN _facturaLN;

    public FacturaController(IFacturaLN facturaLN)
    {
        _facturaLN = facturaLN;
    }

    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<FacturaDTO>>>> ObtenerTodos()
    {
        List<FacturaDTO> facturas =
            await _facturaLN.ObtenerTodosAsync();

        return Ok(
            Respuesta<List<FacturaDTO>>.Exitosa(
                facturas,
                "Facturas consultadas correctamente."
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<FacturaDTO>>> ObtenerPorId(int id)
    {
        FacturaDTO? factura =
            await _facturaLN.ObtenerPorIdAsync(id);

        if (factura is null)
        {
            return NotFound(
                Respuesta<FacturaDTO>.Fallida(
                    "No se encontró la factura."
                )
            );
        }

        return Ok(
            Respuesta<FacturaDTO>.Exitosa(
                factura,
                "Factura consultada correctamente."
            )
        );
    }

    [HttpPost("generar/{ingresoId:int}")]
    public async Task<ActionResult<
        Respuesta<FacturaDTO>>> Generar(
        int ingresoId)
    {
        try
        {
            FacturaDTO factura =
                await _facturaLN.GenerarAsync(
                    ingresoId
                );

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = factura.FacturaId },
                Respuesta<FacturaDTO>.Exitosa(
                    factura,
                    "Factura generada correctamente."
                )
            );
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(
                Respuesta<FacturaDTO>.Fallida(
                    "No se pudo generar la factura.",
                    [ex.Message]
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<FacturaDTO>.Fallida(
                    "No se pudo generar la factura.",
                    [ex.Message]
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                Respuesta<FacturaDTO>.Fallida(
                    "Los datos de la factura no son válidos.",
                    [ex.Message]
                )
            );
        }
    }
}
