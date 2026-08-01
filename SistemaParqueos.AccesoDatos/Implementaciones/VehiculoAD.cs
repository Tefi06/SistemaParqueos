using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class VehiculoAD : IVehiculoAD
{
    private readonly ParqueosContext _context;

    public VehiculoAD(ParqueosContext context)
    {
        _context = context;
    }


    //Obtenemos todos los vehiculos activos ordenados por placa
    public async Task<List<Vehiculo>> ObtenerTodosAsync()
    {
        return await _context.Vehiculos
            .AsNoTracking()
            .Where(vehiculo => vehiculo.Activo)
            .OrderBy(vehiculo => vehiculo.Placa)
            .ToListAsync();
    }

    public async Task<Vehiculo?> ObtenerPorIdAsync(int id)
    {
        return await _context.Vehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                vehiculo => vehiculo.VehiculoId == id
            );
    }

    public async Task<Vehiculo> CrearAsync(
        Vehiculo vehiculo)
    {
        await _context.Vehiculos.AddAsync(vehiculo);
        await _context.SaveChangesAsync();

        return vehiculo;
    }

    public async Task<bool> ActualizarAsync(
        Vehiculo vehiculo)
    {
        _context.Vehiculos.Update(vehiculo);

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        Vehiculo? vehiculo =
            await _context.Vehiculos.FindAsync(id);

        if (vehiculo is null)
        {
            return false;
        }

        vehiculo.Activo = false;
        vehiculo.ActualizadoEn = DateTime.UtcNow;
        vehiculo.ActualizadoPor = "Sistema";

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    public async Task<bool> ExistePlacaAsync(
        string placa,
        int? idExcluir = null)
    {
        string placaLimpia = placa.Trim();

        return await _context.Vehiculos
            .AsNoTracking()
            .AnyAsync(vehiculo =>
                vehiculo.Placa == placaLimpia &&
                (!idExcluir.HasValue ||
                 vehiculo.VehiculoId != idExcluir.Value)
            );
    }

    public async Task<bool> ExisteClienteAsync(
        int clienteId)
    {
        return await _context.Clientes
            .AsNoTracking()
            .AnyAsync(cliente =>
                cliente.ClienteId == clienteId &&
                cliente.Activo
            );
    }

    public async Task<bool> ExisteTipoVehiculoAsync(
        int tipoVehiculoId)
    {
        return await _context.TipoVehiculos
            .AsNoTracking()
            .AnyAsync(tipoVehiculo =>
                tipoVehiculo.TipoVehiculoId ==
                    tipoVehiculoId &&
                tipoVehiculo.Activo
            );
    }
}
