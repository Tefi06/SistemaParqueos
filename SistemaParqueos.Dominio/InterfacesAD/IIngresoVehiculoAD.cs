using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IIngresoVehiculoAD
{
    Task<List<IngresoVehiculo>> ObtenerTodosAsync();

    Task<IngresoVehiculo?> ObtenerPorIdAsync(int id);

    Task<bool> ExisteVehiculoActivoAsync(int vehiculoId);

    Task<bool> VehiculoTieneIngresoActivoAsync(
        int vehiculoId
    );

    Task<EspacioParqueo?> ObtenerEspacioDisponibleAsync(
        int parqueoId
    );

    Task<IngresoVehiculo> CrearAsync(
        IngresoVehiculo ingreso
    );

    Task<bool> RegistrarSalidaAsync(
        int ingresoId,
        DateTime fechaSalida
    );
}