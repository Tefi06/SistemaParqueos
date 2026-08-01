using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IVehiculoLN
{
    Task<List<VehiculoDTO>> ObtenerTodosAsync();

    Task<VehiculoDTO?> ObtenerPorIdAsync(int id);

    Task<VehiculoDTO> CrearAsync(
        VehiculoDTO vehiculoDTO
    );

    Task<bool> ActualizarAsync(
        int id,
        VehiculoDTO vehiculoDTO
    );

    Task<bool> EliminarAsync(int id);
}