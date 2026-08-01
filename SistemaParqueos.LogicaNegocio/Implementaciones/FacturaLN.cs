using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class FacturaLN : IFacturaLN
{
    private readonly IFacturaAD _facturaAD;

    public FacturaLN(IFacturaAD facturaAD)
    {
        _facturaAD = facturaAD;
    }

    public async Task<List<FacturaDTO>>
        ObtenerTodosAsync()
    {
        List<Factura> facturas =
            await _facturaAD.ObtenerTodosAsync();

        return facturas
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<FacturaDTO?> ObtenerPorIdAsync(
        int id)
    {
        Factura? factura =
            await _facturaAD.ObtenerPorIdAsync(id);

        return factura is null
            ? null
            : MapearADTO(factura);
    }

    public async Task<FacturaDTO> GenerarAsync(
        int ingresoId)
    {
        bool facturaExiste =
            await _facturaAD
                .ExisteFacturaPorIngresoAsync(
                    ingresoId
                );

        if (facturaExiste)
        {
            throw new InvalidOperationException(
                "Ya existe una factura para este ingreso."
            );
        }

        IngresoVehiculo? ingreso =
            await _facturaAD
                .ObtenerIngresoParaFacturarAsync(
                    ingresoId
                );

        if (ingreso is null)
        {
            throw new KeyNotFoundException(
                "No se encontró el ingreso indicado."
            );
        }

        if (ingreso.FechaSalida is null ||
            ingreso.Estado != "Finalizado")
        {
            throw new InvalidOperationException(
                "Debe registrar la salida antes de generar la factura."
            );
        }

        Tarifa? tarifa =
            await _facturaAD
                .ObtenerTarifaActivaAsync(
                    ingreso.Vehiculo.TipoVehiculoId
                );

        if (tarifa is null)
        {
            throw new KeyNotFoundException(
                "No existe una tarifa activa para el tipo de vehículo."
            );
        }

        TimeSpan duracion =
            ingreso.FechaSalida.Value -
            ingreso.FechaIngreso;

        if (duracion.TotalHours < 0)
        {
            throw new ArgumentException(
                "La duración del ingreso no puede ser negativa."
            );
        }

        decimal horasExactas =
            (decimal)duracion.TotalHours;

        decimal horasCobradas =
            Math.Max(
                1m,
                Math.Ceiling(horasExactas)
            );

        decimal montoTotal =
            horasCobradas * tarifa.MontoHora;

        if (montoTotal < 0)
        {
            throw new ArgumentException(
                "El monto total no puede ser negativo."
            );
        }

        Factura factura = new()
        {
            IngresoId = ingresoId,
            FechaFactura = DateTime.UtcNow,
            HorasCobradas = horasCobradas,
            MontoTotal = montoTotal,
            CreadoEn = DateTime.UtcNow,
            CreadoPor = "Sistema"
        };

        Factura creada =
            await _facturaAD.CrearAsync(factura);

        return MapearADTO(creada);
    }

    private static FacturaDTO MapearADTO(
        Factura factura)
    {
        return new FacturaDTO
        {
            FacturaId = factura.FacturaId,
            IngresoId = factura.IngresoId,
            FechaFactura = factura.FechaFactura,
            HorasCobradas = factura.HorasCobradas,
            MontoTotal = factura.MontoTotal
        };
    }
}