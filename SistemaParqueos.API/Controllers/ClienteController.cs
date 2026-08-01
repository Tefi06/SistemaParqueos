using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteLN _clienteLN;

    public ClienteController(IClienteLN clienteLN)
    {
        _clienteLN = clienteLN;
    }

   
    [HttpGet]
    public async Task<ActionResult<
        Respuesta<List<ClienteDTO>>>> ObtenerTodos()
    {
        List<ClienteDTO> clientes =
            await _clienteLN.ObtenerTodosAsync();

        return Ok(
            Respuesta<List<ClienteDTO>>.Exitosa(
                clientes,
                "Clientes consultados correctamente."
            )
        );
    }

    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<
        Respuesta<ClienteDTO>>> ObtenerPorId(int id)
    {
        if (id <= 0)
        {
            return BadRequest(
                Respuesta<ClienteDTO>.Fallida(
                    "El identificador no es válido.",
                    new List<string>
                    {
                        "El identificador debe ser mayor que cero."
                    }
                )
            );
        }

        ClienteDTO? cliente =
            await _clienteLN.ObtenerPorIdAsync(id);

        if (cliente is null)
        {
            return NotFound(
                Respuesta<ClienteDTO>.Fallida(
                    "No se encontró el cliente."
                )
            );
        }

        return Ok(
            Respuesta<ClienteDTO>.Exitosa(
                cliente,
                "Cliente consultado correctamente."
            )
        );
    }

   
    [HttpPost]
    public async Task<ActionResult<
        Respuesta<ClienteDTO>>> Crear(
        [FromBody] ClienteDTO clienteDTO)
    {
        try
        {
            ClienteDTO creado =
                await _clienteLN.CrearAsync(clienteDTO);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new
                {
                    id = creado.ClienteId
                },
                Respuesta<ClienteDTO>.Exitosa(
                    creado,
                    "Cliente creado correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<ClienteDTO>.Fallida(
                    "No se pudo crear el cliente.",
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
        [FromBody] ClienteDTO clienteDTO)
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
                await _clienteLN.ActualizarAsync(
                    id,
                    clienteDTO
                );

            if (!actualizado)
            {
                return NotFound(
                    Respuesta<object?>.Fallida(
                        "No se encontró el cliente."
                    )
                );
            }

            return Ok(
                Respuesta<object?>.Exitosa(
                    null,
                    "Se actualizo cliente correctamente."
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(
                Respuesta<object?>.Fallida(
                    "No se pudo actualizar el cliente.",
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
            await _clienteLN.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(
                Respuesta<object?>.Fallida(
                    "No se encontró el cliente."
                )
            );
        }

        return Ok(
            Respuesta<object?>.Exitosa(
                null,
                "Se desactivo cliente correctamente."
            )
        );
    }
}
