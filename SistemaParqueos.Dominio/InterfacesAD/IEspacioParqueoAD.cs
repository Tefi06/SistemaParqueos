using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IEspacioParqueoAD
{
    Task<List<EspacioParqueo>> ObtenerTodosAsync();

    Task<EspacioParqueo?> ObtenerPorIdAsync(int id);

    Task<EspacioParqueo> CrearAsync(
        EspacioParqueo espacioParqueo
    );

    Task<bool> ActualizarAsync(
        EspacioParqueo espacioParqueo
    );

    Task<bool> EliminarAsync(int id);

    //Evita que se repita el mismo numero de parqueo
    Task<bool> ExisteNumeroEspacioAsync(
        int parqueoId,
        string numeroEspacio,
        int? idExcluir = null
    );


    //Revisa que el parqueo exits y este true
    Task<bool> ExisteParqueoAsync(int parqueoId);
}