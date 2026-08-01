using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class TipoVehiculoAD : ITipoVehiculoAD
{
    private readonly ParqueosContext _context;

    public TipoVehiculoAD(ParqueosContext context)
    {
        _context = context;
    }

    public async Task<List<TipoVehiculo>> ObtenerTodosAsync()
    {
        return await _context.TipoVehiculos
            .AsNoTracking()
            .OrderBy(x => x.Descripcion)
            .ToListAsync();
    }

    public async Task<TipoVehiculo?> ObtenerPorIdAsync(int id)
    {
        return await _context.TipoVehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.TipoVehiculoId == id
            );
    }

    public async Task<TipoVehiculo> CrearAsync(
        TipoVehiculo tipoVehiculo
    )
    {
        await _context.TipoVehiculos.AddAsync(tipoVehiculo);
        await _context.SaveChangesAsync();

        return tipoVehiculo;
    }

    public async Task<bool> ActualizarAsync(
        TipoVehiculo tipoVehiculo
    )
    {
        TipoVehiculo? existente =
            await _context.TipoVehiculos
                .FirstOrDefaultAsync(
                    x => x.TipoVehiculoId ==
                         tipoVehiculo.TipoVehiculoId
                );

        if (existente is null)
        {
            return false;
        }

        existente.Descripcion =
            tipoVehiculo.Descripcion.Trim();

        existente.Activo =
            tipoVehiculo.Activo;

        existente.ActualizadoEn =
            tipoVehiculo.ActualizadoEn;

        existente.ActualizadoPor =
            tipoVehiculo.ActualizadoPor;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        TipoVehiculo? existente =
            await _context.TipoVehiculos
                .FirstOrDefaultAsync(
                    x => x.TipoVehiculoId == id
                );

        if (existente is null)
        {
            return false;
        }

        // Eliminación lógica:
        // el registro permanece en la base de datos.
        existente.Activo = false;
        existente.ActualizadoEn = DateTime.Now;
        existente.ActualizadoPor = "Sistema";

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExisteDescripcionAsync(
        string descripcion,
        int? idExcluir = null
    )
    {
        string descripcionLimpia =
            descripcion.Trim();

        return await _context.TipoVehiculos
            .AnyAsync(
                x =>
                    x.Descripcion == descripcionLimpia &&
                    (
                        !idExcluir.HasValue ||
                        x.TipoVehiculoId != idExcluir.Value
                    )
            );
    }
}