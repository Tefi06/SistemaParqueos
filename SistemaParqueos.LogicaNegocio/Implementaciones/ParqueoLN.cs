using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class ParqueoLN : IParqueoLN
{
    private readonly IParqueoAD _parqueoAD;

    public ParqueoLN(IParqueoAD parqueoAD)
    {
        _parqueoAD = parqueoAD;
    }

    public async Task<List<ParqueoDTO>> ObtenerTodosAsync()
    {
        List<Parqueo> parqueos =
            await _parqueoAD.ObtenerTodosAsync();

        return parqueos
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<ParqueoDTO?> ObtenerPorIdAsync(int id)
    {
        Parqueo? parqueo =
            await _parqueoAD.ObtenerPorIdAsync(id);

        if (parqueo is null)
        {
            return null;
        }

        return MapearADTO(parqueo);
    }

    public async Task<ParqueoDTO> CrearAsync(
        ParqueoDTO parqueoDTO)
    {
        ValidarCapacidad(parqueoDTO.CapacidadTotal);

        Parqueo parqueo = new()
        {
            NombreParqueo =
                parqueoDTO.NombreParqueo.Trim(),

            Direccion =
                parqueoDTO.Direccion.Trim(),

            Telefono =
                LimpiarTextoOpcional(parqueoDTO.Telefono),

            CapacidadTotal =
                parqueoDTO.CapacidadTotal,

            Activo =
                parqueoDTO.Activo,

            CreadoEn =
                DateTime.UtcNow,

            CreadoPor =
                "Sistema"
        };

        Parqueo parqueoCreado =
            await _parqueoAD.CrearAsync(parqueo);

        return MapearADTO(parqueoCreado);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        ParqueoDTO parqueoDTO)
    {
        Parqueo? parqueo =
            await _parqueoAD.ObtenerPorIdAsync(id);

        if (parqueo is null)
        {
            return false;
        }

        ValidarCapacidad(parqueoDTO.CapacidadTotal);

        parqueo.NombreParqueo =
            parqueoDTO.NombreParqueo.Trim();

        parqueo.Direccion =
            parqueoDTO.Direccion.Trim();

        parqueo.Telefono =
            LimpiarTextoOpcional(parqueoDTO.Telefono);

        parqueo.CapacidadTotal =
            parqueoDTO.CapacidadTotal;

        parqueo.Activo =
            parqueoDTO.Activo;

        parqueo.ActualizadoEn =
            DateTime.UtcNow;

        parqueo.ActualizadoPor =
            "Sistema";

        return await _parqueoAD.ActualizarAsync(parqueo);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        return await _parqueoAD.EliminarAsync(id);
    }

    private static ParqueoDTO MapearADTO(
        Parqueo parqueo)
    {
        return new ParqueoDTO
        {
            ParqueoId = parqueo.ParqueoId,
            NombreParqueo = parqueo.NombreParqueo,
            Direccion = parqueo.Direccion,
            Telefono = parqueo.Telefono,
            CapacidadTotal = parqueo.CapacidadTotal,
            Activo = parqueo.Activo
        };
    }

    private static void ValidarCapacidad(
        int capacidadTotal)
    {
        if (capacidadTotal <= 0)
        {
            throw new ArgumentException(
                "La capacidad total debe ser mayor que cero."
            );
        }
    }

    private static string? LimpiarTextoOpcional(
        string? texto)
    {
        return string.IsNullOrWhiteSpace(texto)
            ? null
            : texto.Trim();
    }
}
