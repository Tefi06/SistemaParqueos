using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface ITarifaLN
{
    Task<List<TarifaDTO>> ObtenerTodosAsync();

    Task<TarifaDTO?> ObtenerPorIdAsync(int id);

    Task<TarifaDTO> CrearAsync(TarifaDTO tarifaDTO);

    Task<bool> ActualizarAsync(
        int id,
        TarifaDTO tarifaDTO
    );

    Task<bool> EliminarAsync(int id);
}
