using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IParqueoAD
{
    Task<List<Parqueo>> ObtenerTodosAsync();

    Task<Parqueo?> ObtenerPorIdAsync(int id);

    Task<Parqueo> CrearAsync(Parqueo parqueo);

    Task<bool> ActualizarAsync(Parqueo parqueo);

    Task<bool> EliminarAsync(int id);
}