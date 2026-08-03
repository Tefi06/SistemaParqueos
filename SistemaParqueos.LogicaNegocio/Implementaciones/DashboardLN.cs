using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class DashboardLN : IDashboardLN
{
    private readonly IDashboardAD _dashboardAD;


    public DashboardLN(IDashboardAD dashboardAD)
    {
        _dashboardAD = dashboardAD;
    }


    public async Task<DashboardDTO> ObtenerDatosAsync()
    {
        DashboardDTO dashboard = new()
        {
            VehiculosIngresadosHoy =
                await _dashboardAD
                    .ObtenerVehiculosIngresadosHoyAsync(),


            IngresosRegistrados =
                await _dashboardAD
                    .ObtenerIngresosRegistradosAsync(),


            EspaciosDisponibles =
                await _dashboardAD
                    .ObtenerEspaciosDisponiblesAsync(),


            FacturacionDiaria =
                await _dashboardAD
                    .ObtenerFacturacionDiariaAsync(),


            FacturacionMensual =
                await _dashboardAD
                    .ObtenerFacturacionMensualAsync()
        };


        return dashboard;
    }
}