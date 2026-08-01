using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class EspacioParqueoLN : IEspacioParqueoLN
{
    private readonly IEspacioParqueoAD _espacioParqueoAD;

    public EspacioParqueoLN(
        IEspacioParqueoAD espacioParqueoAD)
    {
        _espacioParqueoAD = espacioParqueoAD;
    }

    public async Task<List<EspacioParqueoDTO>>
        ObtenerTodosAsync()
    {
        List<EspacioParqueo> espacios =
            await _espacioParqueoAD.ObtenerTodosAsync();

        return espacios
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<EspacioParqueoDTO?>
        ObtenerPorIdAsync(int id)
    {
        EspacioParqueo? espacio =
            await _espacioParqueoAD.ObtenerPorIdAsync(id);

        if (espacio is null)
        {
            return null;
        }

        return MapearADTO(espacio);
    }

    public async Task<EspacioParqueoDTO> CrearAsync(
        EspacioParqueoDTO espacioParqueoDTO)
    {
        await ValidarParqueoAsync(
            espacioParqueoDTO.ParqueoId
        );

        string numeroEspacio =
            NormalizarNumeroEspacio(
                espacioParqueoDTO.NumeroEspacio
            );

        bool numeroExiste =
            await _espacioParqueoAD
                .ExisteNumeroEspacioAsync(
                    espacioParqueoDTO.ParqueoId,
                    numeroEspacio
                );

        if (numeroExiste)
        {
            throw new InvalidOperationException(
                "Ya existe un espacio con ese número " +
                "dentro del parqueo seleccionado."
            );
        }

        EspacioParqueo espacio = new()
        {
            ParqueoId = espacioParqueoDTO.ParqueoId,
            NumeroEspacio = numeroEspacio,
            Disponible = espacioParqueoDTO.Disponible,
            Activo = espacioParqueoDTO.Activo,
            CreadoEn = DateTime.UtcNow,
            CreadoPor = "Sistema"
        };

        EspacioParqueo espacioCreado =
            await _espacioParqueoAD.CrearAsync(espacio);

        return MapearADTO(espacioCreado);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        EspacioParqueoDTO espacioParqueoDTO)
    {
        EspacioParqueo? espacio =
            await _espacioParqueoAD.ObtenerPorIdAsync(id);

        if (espacio is null)
        {
            return false;
        }

        await ValidarParqueoAsync(
            espacioParqueoDTO.ParqueoId
        );

        string numeroEspacio =
            NormalizarNumeroEspacio(
                espacioParqueoDTO.NumeroEspacio
            );

        bool numeroExiste =
            await _espacioParqueoAD
                .ExisteNumeroEspacioAsync(
                    espacioParqueoDTO.ParqueoId,
                    numeroEspacio,
                    id
                );

        if (numeroExiste)
        {
            throw new InvalidOperationException(
                "Ya existe otro espacio con ese número " +
                "dentro del parqueo seleccionado."
            );
        }

        espacio.ParqueoId =
            espacioParqueoDTO.ParqueoId;

        espacio.NumeroEspacio =
            numeroEspacio;

        espacio.Disponible =
            espacioParqueoDTO.Disponible;

        espacio.Activo =
            espacioParqueoDTO.Activo;

        espacio.ActualizadoEn =
            DateTime.UtcNow;

        espacio.ActualizadoPor =
            "Sistema";

        return await _espacioParqueoAD
            .ActualizarAsync(espacio);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        return await _espacioParqueoAD
            .EliminarAsync(id);
    }

    private async Task ValidarParqueoAsync(
        int parqueoId)
    {
        bool parqueoExiste =
            await _espacioParqueoAD
                .ExisteParqueoAsync(parqueoId);

        if (!parqueoExiste)
        {
            throw new InvalidOperationException(
                "El parqueo seleccionado no existe " +
                "o está inactivo."
            );
        }
    }

    private static string NormalizarNumeroEspacio(
        string numeroEspacio)
    {
        return numeroEspacio
            .Trim()
            .ToUpperInvariant();
    }

    private static EspacioParqueoDTO MapearADTO(
        EspacioParqueo espacio)
    {
        return new EspacioParqueoDTO
        {
            EspacioId = espacio.EspacioId,
            ParqueoId = espacio.ParqueoId,
            NumeroEspacio = espacio.NumeroEspacio,
            Disponible = espacio.Disponible,
            Activo = espacio.Activo
        };
    }
}
