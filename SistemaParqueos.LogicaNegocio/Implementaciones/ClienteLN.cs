using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class ClienteLN : IClienteLN
{
    private readonly IClienteAD _clienteAD;

    public ClienteLN(IClienteAD clienteAD)
    {
        _clienteAD = clienteAD;
    }

    public async Task<List<ClienteDTO>> ObtenerTodosAsync()
    {
        List<Cliente> clientes =
            await _clienteAD.ObtenerTodosAsync();

        return clientes
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<ClienteDTO?> ObtenerPorIdAsync(int id)
    {
        Cliente? cliente =
            await _clienteAD.ObtenerPorIdAsync(id);

        if (cliente is null)
        {
            return null;
        }

        return MapearADTO(cliente);
    }

    public async Task<ClienteDTO> CrearAsync(
        ClienteDTO clienteDTO)
    {
        string cedula = clienteDTO.Cedula.Trim();

        bool cedulaExiste =
            await _clienteAD.ExisteCedulaAsync(cedula);

        if (cedulaExiste)
        {
            throw new InvalidOperationException(
                "Ya existe un cliente con esa cédula."
            );
        }

        Cliente cliente = new()
        {
            Nombre = clienteDTO.Nombre.Trim(),
            Apellidos = clienteDTO.Apellidos.Trim(),
            Cedula = cedula,
            Telefono = LimpiarTextoOpcional(
                clienteDTO.Telefono
            ),
            Correo = LimpiarTextoOpcional(
                clienteDTO.Correo
            ),
            Activo = clienteDTO.Activo,
            CreadoEn = DateTime.UtcNow,
            CreadoPor = "Sistema"
        };

        Cliente clienteCreado =
            await _clienteAD.CrearAsync(cliente);

        return MapearADTO(clienteCreado);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        ClienteDTO clienteDTO)
    {
        Cliente? cliente =
            await _clienteAD.ObtenerPorIdAsync(id);

        if (cliente is null)
        {
            return false;
        }

        string cedula = clienteDTO.Cedula.Trim();

        bool cedulaExiste =
            await _clienteAD.ExisteCedulaAsync(
                cedula,
                id
            );

        if (cedulaExiste)
        {
            throw new InvalidOperationException(
                "Ya existe otro cliente con esa cédula."
            );
        }

        cliente.Nombre = clienteDTO.Nombre.Trim();
        cliente.Apellidos = clienteDTO.Apellidos.Trim();
        cliente.Cedula = cedula;
        cliente.Telefono = LimpiarTextoOpcional(
            clienteDTO.Telefono
        );
        cliente.Correo = LimpiarTextoOpcional(
            clienteDTO.Correo
        );
        cliente.Activo = clienteDTO.Activo;
        cliente.ActualizadoEn = DateTime.UtcNow;
        cliente.ActualizadoPor = "Sistema";

        return await _clienteAD.ActualizarAsync(cliente);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        return await _clienteAD.EliminarAsync(id);
    }

    private static ClienteDTO MapearADTO(
        Cliente cliente)
    {
        return new ClienteDTO
        {
            ClienteId = cliente.ClienteId,
            Nombre = cliente.Nombre,
            Apellidos = cliente.Apellidos,
            Cedula = cliente.Cedula,
            Telefono = cliente.Telefono,
            Correo = cliente.Correo,
            Activo = cliente.Activo
        };
    }

    private static string? LimpiarTextoOpcional(
        string? texto)
    {
        return string.IsNullOrWhiteSpace(texto)
            ? null
            : texto.Trim();
    }
}