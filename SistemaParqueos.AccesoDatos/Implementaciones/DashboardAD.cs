using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.Dominio.InterfacesAD;

namespace SistemaParqueos.AccesoDatos.Implementaciones;

public class DashboardAD : IDashboardAD
{
    private readonly ParqueosContext _context;

    public DashboardAD(ParqueosContext context)
    {
        _context = context;
    }


    public async Task<int> ObtenerVehiculosIngresadosHoyAsync()
    {
        DateTime inicioDia = DateTime.Today;
        DateTime finDia = inicioDia.AddDays(1);

        return await _context.IngresoVehiculos
            .CountAsync(ingreso =>
                ingreso.FechaIngreso >= inicioDia &&
                ingreso.FechaIngreso < finDia
            );
    }


    public async Task<int> ObtenerIngresosRegistradosAsync()
    {
        return await _context.IngresoVehiculos
            .CountAsync();
    }


    public async Task<int> ObtenerEspaciosDisponiblesAsync()
    {
        return await _context.EspacioParqueos
            .CountAsync(espacio =>
                espacio.Disponible &&
                espacio.Activo
            );
    }


    public async Task<decimal> ObtenerFacturacionDiariaAsync()
    {
        DateTime ahoraCostaRica =
            TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                DateTime.UtcNow,
                "Central America Standard Time"
            );


        DateTime inicioDiaCostaRica =
            ahoraCostaRica.Date;


        DateTime finDiaCostaRica =
            inicioDiaCostaRica.AddDays(1);


        DateTime inicioUtc =
            inicioDiaCostaRica.AddHours(6);


        DateTime finUtc =
            finDiaCostaRica.AddHours(6);


        return await _context.Facturas
            .Where(factura =>
                factura.FechaFactura >= inicioUtc &&
                factura.FechaFactura < finUtc
            )
            .SumAsync(factura =>
                factura.MontoTotal
            );
    }


    public async Task<decimal> ObtenerFacturacionMensualAsync()
    {
        DateTime inicioMes = new(
            DateTime.Today.Year,
            DateTime.Today.Month,
            1
        );

        DateTime finMes =
            inicioMes.AddMonths(1);


        return await _context.Facturas
            .Where(factura =>
                factura.FechaFactura >= inicioMes &&
                factura.FechaFactura < finMes
            )
            .SumAsync(factura =>
                factura.MontoTotal
            );
    }
}