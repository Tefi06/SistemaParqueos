using System;
using System.Collections.Generic;
using System.Text;
using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class TipoVehiculoLN : ITipoVehiculoLN
{
    private readonly ITipoVehiculoAD _tipoVehiculoAD;

    public TipoVehiculoLN(ITipoVehiculoAD tipoVehiculoAD)
    {
        _tipoVehiculoAD = tipoVehiculoAD;
    }

    public async Task<List<TipoVehiculoDTO>> ObtenerTodosAsync()
    {
        List<TipoVehiculo> tiposVehiculo =
            await _tipoVehiculoAD.ObtenerTodosAsync();

        return tiposVehiculo
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<TipoVehiculoDTO?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        TipoVehiculo? tipoVehiculo =
            await _tipoVehiculoAD.ObtenerPorIdAsync(id);

        if (tipoVehiculo is null)
        {
            return null;
        }

        return MapearADTO(tipoVehiculo);
    }

    public async Task<TipoVehiculoDTO> CrearAsync(
        TipoVehiculoCrearDTO tipoVehiculoDTO
    )
    {
        string descripcion =
            tipoVehiculoDTO.Descripcion.Trim();

        bool existe =
            await _tipoVehiculoAD.ExisteDescripcionAsync(
                descripcion
            );

        if (existe)
        {
            throw new InvalidOperationException(
                "Ya existe un tipo de vehículo con esa descripción."
            );
        }

        TipoVehiculo tipoVehiculo = new()
        {
            Descripcion = descripcion,
            Activo = tipoVehiculoDTO.Activo,
            CreadoEn = DateTime.UtcNow,
            CreadoPor = "Sistema"
        };

        TipoVehiculo creado =
            await _tipoVehiculoAD.CrearAsync(tipoVehiculo);

        return MapearADTO(creado);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        TipoVehiculoActualizarDTO tipoVehiculoDTO
    )
    {
        if (id <= 0)
        {
            return false;
        }

        TipoVehiculo? existente =
            await _tipoVehiculoAD.ObtenerPorIdAsync(id);

        if (existente is null)
        {
            return false;
        }

        string descripcion =
            tipoVehiculoDTO.Descripcion.Trim();

        bool descripcionRepetida =
            await _tipoVehiculoAD.ExisteDescripcionAsync(
                descripcion,
                id
            );

        if (descripcionRepetida)
        {
            throw new InvalidOperationException(
                "Ya existe otro tipo de vehículo con esa descripción."
            );
        }

        TipoVehiculo tipoVehiculo = new()
        {
            TipoVehiculoId = id,
            Descripcion = descripcion,
            Activo = tipoVehiculoDTO.Activo,
            ActualizadoEn = DateTime.UtcNow,
            ActualizadoPor = "Sistema"
        };

        return await _tipoVehiculoAD.ActualizarAsync(
            tipoVehiculo
        );
    }

    public async Task<bool> EliminarAsync(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        return await _tipoVehiculoAD.EliminarAsync(id);
    }

    private static TipoVehiculoDTO MapearADTO(
        TipoVehiculo tipoVehiculo
    )
    {
        return new TipoVehiculoDTO
        {
            TipoVehiculoId =
                tipoVehiculo.TipoVehiculoId,

            Descripcion =
                tipoVehiculo.Descripcion,

            Activo =
                tipoVehiculo.Activo
        };
    }
}
