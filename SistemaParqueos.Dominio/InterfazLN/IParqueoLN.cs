using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IParqueoLN
{
    Task<List<ParqueoDTO>> ObtenerTodosAsync();

    Task<ParqueoDTO?> ObtenerPorIdAsync(int id);

    Task<ParqueoDTO> CrearAsync(
        ParqueoDTO parqueoDTO
    );

    Task<bool> ActualizarAsync(
        int id,
        ParqueoDTO parqueoDTO
    );

    Task<bool> EliminarAsync(int id);
}