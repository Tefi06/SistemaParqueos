using Microsoft.AspNetCore.Mvc;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardLN _dashboardLN;


    public DashboardController(
        IDashboardLN dashboardLN)
    {
        _dashboardLN = dashboardLN;
    }


    [HttpGet]
    public async Task<ActionResult<
        Respuesta<DashboardDTO>>> Obtener()
    {
        DashboardDTO dashboard =
            await _dashboardLN
                .ObtenerDatosAsync();


        return Ok(
            Respuesta<DashboardDTO>.Exitosa(
                dashboard,
                "Datos del dashboard consultados correctamente."
            )
        );
    }
}