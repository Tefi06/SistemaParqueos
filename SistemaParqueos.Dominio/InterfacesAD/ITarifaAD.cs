using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface ITarifaAD
{
    Task<List<Tarifa>> ObtenerTodosAsync();

    Task<Tarifa?> ObtenerPorIdAsync(int id);

    Task<Tarifa> CrearAsync(Tarifa tarifa);

    Task<bool> ActualizarAsync(Tarifa tarifa);

    Task<bool> EliminarAsync(int id);

    Task<bool> ExisteTipoVehiculoAsync(int tipoVehiculoId);
}