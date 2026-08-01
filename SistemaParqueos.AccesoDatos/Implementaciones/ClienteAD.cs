using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class ClienteAD : IClienteAD
{
    private readonly ParqueosContext _context;

    public ClienteAD(ParqueosContext context)
    {
        _context = context;
    }

    //Con esto obtengo los clientes activos
    public async Task<List<Cliente>> ObtenerTodosAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where(cliente => cliente.Activo)
            .OrderBy(cliente => cliente.Apellidos)
            .ThenBy(cliente => cliente.Nombre)
            .ToListAsync();
    }

    //Aqui busco por su Id
    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(
                cliente => cliente.ClienteId == id
            );
    }

    //Creo un cleinte
    public async Task<Cliente> CrearAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }

    //Actualizo los datos del cliente
    public async Task<bool> ActualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    //Cambio de activo a false
    public async Task<bool> EliminarAsync(int id)
    {
        Cliente? cliente =
            await _context.Clientes.FindAsync(id);

        if (cliente is null)
        {
            return false;
        }

        cliente.Activo = false;
        cliente.ActualizadoEn = DateTime.UtcNow;
        cliente.ActualizadoPor = "Sistema";

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    //Se evita repetir cedula
    public async Task<bool> ExisteCedulaAsync(
        string cedula,
        int? idExcluir = null)
    {
        string cedulaLimpia = cedula.Trim();

        return await _context.Clientes
            .AsNoTracking()
            .AnyAsync(cliente =>
                cliente.Cedula == cedulaLimpia &&
                (!idExcluir.HasValue ||
                 cliente.ClienteId != idExcluir.Value)
            );
    }
}