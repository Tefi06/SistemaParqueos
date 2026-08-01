using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class TarifaLN : ITarifaLN
{
    private readonly ITarifaAD _tarifaAD;

    public TarifaLN(ITarifaAD tarifaAD)
    {
        _tarifaAD = tarifaAD;
    }

    public async Task<List<TarifaDTO>> ObtenerTodosAsync()
    {
        List<Tarifa> tarifas =
            await _tarifaAD.ObtenerTodosAsync();

        return tarifas
            .Select(MapearADTO)
            .ToList();
    }

    public async Task<TarifaDTO?> ObtenerPorIdAsync(int id)
    {
        Tarifa? tarifa =
            await _tarifaAD.ObtenerPorIdAsync(id);

        return tarifa is null
            ? null
            : MapearADTO(tarifa);
    }

    public async Task<TarifaDTO> CrearAsync(
        TarifaDTO tarifaDTO)
    {
        await ValidarTipoVehiculoAsync(
            tarifaDTO.TipoVehiculoId
        );

        ValidarMonto(tarifaDTO.MontoHora);

        Tarifa tarifa = new()
        {
            TipoVehiculoId = tarifaDTO.TipoVehiculoId,
            Descripcion = tarifaDTO.Descripcion.Trim(),
            MontoHora = tarifaDTO.MontoHora,
            Activo = tarifaDTO.Activo,
            CreadoEn = DateTime.UtcNow,
            CreadoPor = "Sistema"
        };

        Tarifa creada =
            await _tarifaAD.CrearAsync(tarifa);

        return MapearADTO(creada);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        TarifaDTO tarifaDTO)
    {
        Tarifa? tarifa =
            await _tarifaAD.ObtenerPorIdAsync(id);

        if (tarifa is null)
        {
            return false;
        }

        await ValidarTipoVehiculoAsync(
            tarifaDTO.TipoVehiculoId
        );

        ValidarMonto(tarifaDTO.MontoHora);

        tarifa.TipoVehiculoId =
            tarifaDTO.TipoVehiculoId;

        tarifa.Descripcion =
            tarifaDTO.Descripcion.Trim();

        tarifa.MontoHora =
            tarifaDTO.MontoHora;

        tarifa.Activo =
            tarifaDTO.Activo;

        tarifa.ActualizadoEn =
            DateTime.UtcNow;

        tarifa.ActualizadoPor =
            "Sistema";

        return await _tarifaAD.ActualizarAsync(tarifa);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        return await _tarifaAD.EliminarAsync(id);
    }

    private async Task ValidarTipoVehiculoAsync(
        int tipoVehiculoId)
    {
        bool existe =
            await _tarifaAD.ExisteTipoVehiculoAsync(
                tipoVehiculoId
            );

        if (!existe)
        {
            throw new InvalidOperationException(
                "El tipo de vehículo seleccionado no existe o está inactivo."
            );
        }
    }

    private static void ValidarMonto(decimal montoHora)
    {
        if (montoHora <= 0)
        {
            throw new ArgumentException(
                "El monto por hora debe ser mayor que cero."
            );
        }
    }

    private static TarifaDTO MapearADTO(Tarifa tarifa)
    {
        return new TarifaDTO
        {
            TarifaId = tarifa.TarifaId,
            TipoVehiculoId = tarifa.TipoVehiculoId,
            Descripcion = tarifa.Descripcion,
            MontoHora = tarifa.MontoHora,
            Activo = tarifa.Activo
        };
    }
}
