using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class FacturaAD : IFacturaAD
{
    private readonly ParqueosContext _context;

    public FacturaAD(ParqueosContext context)
    {
        _context = context;
    }

    public async Task<List<Factura>> ObtenerTodosAsync()
    {
        return await _context.Facturas

            .AsNoTracking()

            .Include(factura => factura.Ingreso)

                .ThenInclude(ingreso => ingreso.Vehiculo)

                    .ThenInclude(vehiculo => vehiculo.Cliente)

            .OrderByDescending(
                factura => factura.FechaFactura
            )

            .ToListAsync();
    }

    public async Task<Factura?> ObtenerPorIdAsync(int id)
    {
        return await _context.Facturas

            .AsNoTracking()

            .Include(factura => factura.Ingreso)

                .ThenInclude(ingreso => ingreso.Vehiculo)

                    .ThenInclude(vehiculo => vehiculo.Cliente)

            .FirstOrDefaultAsync(
                factura => factura.FacturaId == id
            );
    }

    public async Task<bool>
        ExisteFacturaPorIngresoAsync(int ingresoId)
    {
        return await _context.Facturas
            .AsNoTracking()
            .AnyAsync(
                factura => factura.IngresoId == ingresoId
            );
    }

    public async Task<IngresoVehiculo?>
        ObtenerIngresoParaFacturarAsync(int ingresoId)
    {
        return await _context.IngresoVehiculos
            .AsNoTracking()
            .Include(ingreso => ingreso.Vehiculo)
            .FirstOrDefaultAsync(
                ingreso => ingreso.IngresoId == ingresoId
            );
    }

    public async Task<Tarifa?> ObtenerTarifaActivaAsync(
        int tipoVehiculoId)
    {
        return await _context.Tarifas
            .AsNoTracking()
            .Where(tarifa =>
                tarifa.TipoVehiculoId ==
                    tipoVehiculoId &&
                tarifa.Activo
            )
            .OrderByDescending(
                tarifa => tarifa.TarifaId
            )
            .FirstOrDefaultAsync();
    }

    public async Task<Factura> CrearAsync(
        Factura factura)
    {
        await _context.Facturas.AddAsync(factura);
        await _context.SaveChangesAsync();

        return factura;
    }
}
