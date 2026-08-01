using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IEspacioParqueoLN
{
    Task<List<EspacioParqueoDTO>> ObtenerTodosAsync();

    Task<EspacioParqueoDTO?> ObtenerPorIdAsync(int id);

    Task<EspacioParqueoDTO> CrearAsync(
        EspacioParqueoDTO espacioParqueoDTO
    );

    Task<bool> ActualizarAsync(
        int id,
        EspacioParqueoDTO espacioParqueoDTO
    );

    Task<bool> EliminarAsync(int id);
}
