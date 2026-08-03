using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class IngresoVehiculoAD : IIngresoVehiculoAD
{
    private readonly ParqueosContext _context;

    public IngresoVehiculoAD(ParqueosContext context)
    {
        _context = context;
    }

    public async Task<List<IngresoVehiculo>>
        ObtenerTodosAsync()
    {
        return await _context.IngresoVehiculos
            .AsNoTracking()
            .Include(ingreso => ingreso.Espacio)
            .OrderByDescending(
                ingreso => ingreso.FechaIngreso
            )
            .ToListAsync();
    }

    public async Task<IngresoVehiculo?>
        ObtenerPorIdAsync(int id)
    {
        return await _context.IngresoVehiculos
            .AsNoTracking()
            .Include(ingreso => ingreso.Espacio)
            .FirstOrDefaultAsync(
                ingreso => ingreso.IngresoId == id
            );
    }

    public async Task<bool> ExisteVehiculoActivoAsync(
        int vehiculoId)
    {
        return await _context.Vehiculos
            .AsNoTracking()
            .AnyAsync(vehiculo =>
                vehiculo.VehiculoId == vehiculoId &&
                vehiculo.Activo
            );
    }

    public async Task<bool>
        VehiculoTieneIngresoActivoAsync(int vehiculoId)
    {
        return await _context.IngresoVehiculos
            .AsNoTracking()
            .AnyAsync(ingreso =>
                ingreso.VehiculoId == vehiculoId &&
                ingreso.Estado == "Activo" &&
                ingreso.FechaSalida == null
            );
    }

    public async Task<EspacioParqueo?>
        ObtenerEspacioDisponibleAsync(int parqueoId)
    {
        return await _context.EspacioParqueos
            .Where(espacio =>
                espacio.ParqueoId == parqueoId &&
                espacio.Activo &&
                espacio.Disponible &&
                espacio.Parqueo.Activo
            )
            .OrderBy(espacio => espacio.NumeroEspacio)
            .FirstOrDefaultAsync();
    }

    public async Task<IngresoVehiculo> CrearAsync(
        IngresoVehiculo ingreso)
    {
        await _context.IngresoVehiculos.AddAsync(
            ingreso
        );

        await _context.SaveChangesAsync();

        return ingreso;
    }

    public async Task<bool> RegistrarSalidaAsync(
        int ingresoId,
        DateTime fechaSalida)
    {
        IngresoVehiculo? ingreso =
            await _context.IngresoVehiculos
                .Include(item => item.Espacio)
                .FirstOrDefaultAsync(
                    item => item.IngresoId == ingresoId
                );

        if (ingreso is null ||
            ingreso.FechaSalida is not null ||
            ingreso.Estado != "Activo")
        {
            return false;
        }

        ingreso.FechaSalida = fechaSalida;
        ingreso.Estado = "Finalizado";
        ingreso.ActualizadoEn = fechaSalida;
        ingreso.ActualizadoPor = "Sistema";

        ingreso.Espacio.Disponible = true;
        ingreso.Espacio.ActualizadoEn = fechaSalida;
        ingreso.Espacio.ActualizadoPor = "Sistema";

        int filasAfectadas =
            await _context.SaveChangesAsync();

        return filasAfectadas > 0;
    }
}
