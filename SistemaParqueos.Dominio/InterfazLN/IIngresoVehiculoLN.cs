using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IIngresoVehiculoLN
{
    Task<List<IngresoVehiculoDTO>> ObtenerTodosAsync();

    Task<IngresoVehiculoDTO?> ObtenerPorIdAsync(int id);

    Task<IngresoVehiculoDTO> RegistrarIngresoAsync(
        IngresoVehiculoDTO ingresoDTO
    );

    Task<IngresoVehiculoDTO?> RegistrarSalidaAsync(
        int id
    );
}