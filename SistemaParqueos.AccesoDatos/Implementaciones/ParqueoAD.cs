using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class ParqueoAD : IParqueoAD
{
    private readonly ParqueosContext _context;

    public ParqueoAD(ParqueosContext context)
    {
        _context = context;
    }

    //Obtener todos los parquos activos
    public async Task<List<Parqueo>> ObtenerTodosAsync()
    {
        return await _context.Parqueos
             .Include(p => p.EspacioParqueos)
            .AsNoTracking()
            .Where(parqueo => parqueo.Activo)
            .OrderBy(parqueo => parqueo.NombreParqueo)
            .ToListAsync();
    }

    //Obtener parqueos por Id
    public async Task<Parqueo?> ObtenerPorIdAsync(int id)
    {
        return await _context.Parqueos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                parqueo => parqueo.ParqueoId == id
            );
    }

    //Crear un parqueo
    public async Task<Parqueo> CrearAsync(Parqueo parqueo)
    {
        await _context.Parqueos.AddAsync(parqueo);
        await _context.SaveChangesAsync();

        return parqueo;
    }

    //Actualizar parqueo
    public async Task<bool> ActualizarAsync(Parqueo parqueo)
    {
        _context.Parqueos.Update(parqueo);

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        Parqueo? parqueo =
            await _context.Parqueos.FindAsync(id);

        if (parqueo is null)
        {
            return false;
        }

        parqueo.Activo = false;
        parqueo.ActualizadoEn = DateTime.UtcNow;
        parqueo.ActualizadoPor = "Sistema";

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }
}