using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IClienteLN
{
    Task<List<ClienteDTO>> ObtenerTodosAsync();

    Task<ClienteDTO?> ObtenerPorIdAsync(int id);

    Task<ClienteDTO> CrearAsync(ClienteDTO clienteDTO);

    Task<bool> ActualizarAsync(
        int id,
        ClienteDTO clienteDTO
    );

    Task<bool> EliminarAsync(int id);
}