using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class VehiculoLN : IVehiculoLN
{
    private readonly IVehiculoAD _vehiculoAD;

    public VehiculoLN(IVehiculoAD vehiculoAD)
    {
        _vehiculoAD = vehiculoAD;
    }

    public async Task<List<VehiculoDTO>> ObtenerTodosAsync()
    {
        List<Vehiculo> vehiculos =
            await _vehiculoAD.ObtenerTodosAsync();

        return vehiculos
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<VehiculoDTO?> ObtenerPorIdAsync(int id)
    {
        Vehiculo? vehiculo =
            await _vehiculoAD.ObtenerPorIdAsync(id);

        if (vehiculo is null)
        {
            return null;
        }

        return MapearADTO(vehiculo);
    }

    public async Task<VehiculoDTO> CrearAsync(
        VehiculoDTO vehiculoDTO)
    {
        await ValidarRelacionesAsync(
            vehiculoDTO.ClienteId,
            vehiculoDTO.TipoVehiculoId
        );

        string placa = NormalizarPlaca(vehiculoDTO.Placa);

        bool placaExiste =
            await _vehiculoAD.ExistePlacaAsync(placa);

        if (placaExiste)
        {
            throw new InvalidOperationException(
                "Ya existe un vehículo con esa placa."
            );
        }

        Vehiculo vehiculo = new()
        {
            ClienteId = vehiculoDTO.ClienteId,
            TipoVehiculoId = vehiculoDTO.TipoVehiculoId,
            Placa = placa,
            Marca = vehiculoDTO.Marca.Trim(),
            Modelo = LimpiarTextoOpcional(
                vehiculoDTO.Modelo
            ),
            Color = LimpiarTextoOpcional(
                vehiculoDTO.Color
            ),
            Activo = vehiculoDTO.Activo,
            CreadoEn = DateTime.UtcNow,
            CreadoPor = "Sistema"
        };

        Vehiculo vehiculoCreado =
            await _vehiculoAD.CrearAsync(vehiculo);

        return MapearADTO(vehiculoCreado);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        VehiculoDTO vehiculoDTO)
    {
        Vehiculo? vehiculo =
            await _vehiculoAD.ObtenerPorIdAsync(id);

        if (vehiculo is null)
        {
            return false;
        }

        await ValidarRelacionesAsync(
            vehiculoDTO.ClienteId,
            vehiculoDTO.TipoVehiculoId
        );

        string placa = NormalizarPlaca(vehiculoDTO.Placa);

        bool placaExiste =
            await _vehiculoAD.ExistePlacaAsync(
                placa,
                id
            );

        if (placaExiste)
        {
            throw new InvalidOperationException(
                "Ya existe otro vehículo con esa placa."
            );
        }

        vehiculo.ClienteId = vehiculoDTO.ClienteId;
        vehiculo.TipoVehiculoId =
            vehiculoDTO.TipoVehiculoId;

        vehiculo.Placa = placa;
        vehiculo.Marca = vehiculoDTO.Marca.Trim();

        vehiculo.Modelo =
            LimpiarTextoOpcional(vehiculoDTO.Modelo);

        vehiculo.Color =
            LimpiarTextoOpcional(vehiculoDTO.Color);

        vehiculo.Activo = vehiculoDTO.Activo;
        vehiculo.ActualizadoEn = DateTime.UtcNow;
        vehiculo.ActualizadoPor = "Sistema";

        return await _vehiculoAD.ActualizarAsync(vehiculo);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        return await _vehiculoAD.EliminarAsync(id);
    }

    private async Task ValidarRelacionesAsync(
        int clienteId,
        int tipoVehiculoId)
    {
        bool clienteExiste =
            await _vehiculoAD.ExisteClienteAsync(clienteId);

        if (!clienteExiste)
        {
            throw new InvalidOperationException(
                "El cliente seleccionado no existe o está inactivo."
            );
        }

        bool tipoVehiculoExiste =
            await _vehiculoAD.ExisteTipoVehiculoAsync(
                tipoVehiculoId
            );

        if (!tipoVehiculoExiste)
        {
            throw new InvalidOperationException(
                "El tipo de vehículo seleccionado no existe o está inactivo."
            );
        }
    }

    private static VehiculoDTO MapearADTO(
        Vehiculo vehiculo)
    {
        return new VehiculoDTO
        {
            VehiculoId = vehiculo.VehiculoId,
            ClienteId = vehiculo.ClienteId,
            TipoVehiculoId = vehiculo.TipoVehiculoId,
            Placa = vehiculo.Placa,
            Marca = vehiculo.Marca,
            Modelo = vehiculo.Modelo,
            Color = vehiculo.Color,
            Activo = vehiculo.Activo
        };
    }

    private static string NormalizarPlaca(string placa)
    {
        return placa
            .Trim()
            .ToUpperInvariant();
    }

    private static string? LimpiarTextoOpcional(
        string? texto)
    {
        return string.IsNullOrWhiteSpace(texto)
            ? null
            : texto.Trim();
    }
}
