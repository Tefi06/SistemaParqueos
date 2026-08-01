using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IVehiculoAD
{
    Task<List<Vehiculo>> ObtenerTodosAsync();

    Task<Vehiculo?> ObtenerPorIdAsync(int id);

    Task<Vehiculo> CrearAsync(Vehiculo vehiculo);

    Task<bool> ActualizarAsync(Vehiculo vehiculo);

    Task<bool> EliminarAsync(int id);

    Task<bool> ExistePlacaAsync(
        string placa,
        int? idExcluir = null
    );

    Task<bool> ExisteClienteAsync(int clienteId);

    Task<bool> ExisteTipoVehiculoAsync(int tipoVehiculoId);
}
