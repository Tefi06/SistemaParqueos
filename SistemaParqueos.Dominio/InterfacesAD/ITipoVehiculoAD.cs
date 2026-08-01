using System;
using System.Collections.Generic;
using System.Text;
using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface ITipoVehiculoAD
{
    Task<List<TipoVehiculo>> ObtenerTodosAsync();

    Task<TipoVehiculo?> ObtenerPorIdAsync(int id);

    Task<TipoVehiculo> CrearAsync(TipoVehiculo tipoVehiculo);

    Task<bool> ActualizarAsync(TipoVehiculo tipoVehiculo);

    Task<bool> EliminarAsync(int id);

    Task<bool> ExisteDescripcionAsync(
        string descripcion,
        int? idExcluir = null
    );
}