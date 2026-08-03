using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IDashboardAD
{
    Task<int> ObtenerVehiculosIngresadosHoyAsync();

    Task<int> ObtenerIngresosRegistradosAsync();

    Task<int> ObtenerEspaciosDisponiblesAsync();

    Task<decimal> ObtenerFacturacionDiariaAsync();

    Task<decimal> ObtenerFacturacionMensualAsync();
}