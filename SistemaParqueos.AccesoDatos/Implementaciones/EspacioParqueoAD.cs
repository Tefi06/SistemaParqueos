using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class EspacioParqueoAD : IEspacioParqueoAD
{
    private readonly ParqueosContext _context;

    public EspacioParqueoAD(ParqueosContext context)
    {
        _context = context;
    }

    public async Task<List<EspacioParqueo>> ObtenerTodosAsync()
    {
        return await _context.EspacioParqueos
            .AsNoTracking()
            .Where(espacio => espacio.Activo)
            .OrderBy(espacio => espacio.ParqueoId)
            .ThenBy(espacio => espacio.NumeroEspacio)
            .ToListAsync();
    }

    public async Task<EspacioParqueo?> ObtenerPorIdAsync(int id)
    {
        return await _context.EspacioParqueos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                espacio => espacio.EspacioId == id
            );
    }

    public async Task<EspacioParqueo> CrearAsync(
        EspacioParqueo espacioParqueo)
    {
        await _context.EspacioParqueos.AddAsync(
            espacioParqueo
        );

        await _context.SaveChangesAsync();

        return espacioParqueo;
    }

    public async Task<bool> ActualizarAsync(
        EspacioParqueo espacioParqueo)
    {
        _context.EspacioParqueos.Update(
            espacioParqueo
        );

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        EspacioParqueo? espacio =
            await _context.EspacioParqueos.FindAsync(id);

        if (espacio is null)
        {
            return false;
        }

        espacio.Activo = false;
        espacio.Disponible = false;
        espacio.ActualizadoEn = DateTime.UtcNow;
        espacio.ActualizadoPor = "Sistema";

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    public async Task<bool> ExisteNumeroEspacioAsync(
        int parqueoId,
        string numeroEspacio,
        int? idExcluir = null)
    {
        string numeroLimpio =
            numeroEspacio.Trim().ToUpperInvariant();

        return await _context.EspacioParqueos
            .AsNoTracking()
            .AnyAsync(espacio =>
                espacio.ParqueoId == parqueoId &&
                espacio.NumeroEspacio == numeroLimpio &&
                (!idExcluir.HasValue ||
                 espacio.EspacioId != idExcluir.Value)
            );
    }

    public async Task<bool> ExisteParqueoAsync(
        int parqueoId)
    {
        return await _context.Parqueos
            .AsNoTracking()
            .AnyAsync(parqueo =>
                parqueo.ParqueoId == parqueoId &&
                parqueo.Activo
            );
    }
}