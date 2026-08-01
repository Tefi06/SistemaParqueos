using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.Dominio.InterfacesAD;

public interface IFacturaAD
{
    Task<List<Factura>> ObtenerTodosAsync();

    Task<Factura?> ObtenerPorIdAsync(int id);

    Task<bool> ExisteFacturaPorIngresoAsync(
        int ingresoId
    );

    Task<IngresoVehiculo?>
        ObtenerIngresoParaFacturarAsync(
            int ingresoId
        );

    Task<Tarifa?> ObtenerTarifaActivaAsync(
        int tipoVehiculoId
    );

    Task<Factura> CrearAsync(Factura factura);
}