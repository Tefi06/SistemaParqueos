using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IClienteAD
{
    Task<List<Cliente>> ObtenerTodosAsync();

    Task<Cliente?> ObtenerPorIdAsync(int id);

    Task<Cliente> CrearAsync(Cliente cliente);

    Task<bool> ActualizarAsync(Cliente cliente);

    Task<bool> EliminarAsync(int id);

    //Este metodo sirve para validar que no se registre la misma cedula dos veces. 
   
    Task<bool> ExisteCedulaAsync(
        string cedula,
        int? idExcluir = null //Sirve para actualizar el cleinte sin que se detecte duplicado 
    );
}