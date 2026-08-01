using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IFacturaLN
{
    Task<List<FacturaDTO>> ObtenerTodosAsync();

    Task<FacturaDTO?> ObtenerPorIdAsync(int id);

    Task<FacturaDTO> GenerarAsync(int ingresoId);
}