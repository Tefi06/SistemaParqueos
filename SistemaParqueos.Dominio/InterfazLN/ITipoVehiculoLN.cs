using System;
using System.Collections.Generic;
using System.Text;
using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface ITipoVehiculoLN
{
    Task<List<TipoVehiculoDTO>> ObtenerTodosAsync();

    Task<TipoVehiculoDTO?> ObtenerPorIdAsync(int id);

    Task<TipoVehiculoDTO> CrearAsync(
        TipoVehiculoCrearDTO tipoVehiculoDTO
    );

    Task<bool> ActualizarAsync(
        int id,
        TipoVehiculoActualizarDTO tipoVehiculoDTO
    );

    Task<bool> EliminarAsync(int id);
}
