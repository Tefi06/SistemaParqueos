using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IDashboardLN
{
    Task<DashboardDTO> ObtenerDatosAsync();
}