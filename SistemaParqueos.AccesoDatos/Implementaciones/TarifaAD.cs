using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class TarifaAD : ITarifaAD
{
    private readonly ParqueosContext _context;

    public TarifaAD(ParqueosContext context)
    {
        _context = context;
    }

    public async Task<List<Tarifa>> ObtenerTodosAsync()
    {
        return await _context.Tarifas
            .AsNoTracking()
            .Where(tarifa => tarifa.Activo)
            .OrderBy(tarifa => tarifa.Descripcion)
            .ToListAsync();
    }

    public async Task<Tarifa?> ObtenerPorIdAsync(int id)
    {
        return await _context.Tarifas
            .AsNoTracking()
            .FirstOrDefaultAsync(
                tarifa => tarifa.TarifaId == id
            );
    }

    public async Task<Tarifa> CrearAsync(Tarifa tarifa)
    {
        await _context.Tarifas.AddAsync(tarifa);
        await _context.SaveChangesAsync();

        return tarifa;
    }

    public async Task<bool> ActualizarAsync(Tarifa tarifa)
    {
        _context.Tarifas.Update(tarifa);

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        Tarifa? tarifa =
            await _context.Tarifas.FindAsync(id);

        if (tarifa is null)
        {
            return false;
        }

        tarifa.Activo = false;
        tarifa.ActualizadoEn = DateTime.UtcNow;
        tarifa.ActualizadoPor = "Sistema";

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
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
