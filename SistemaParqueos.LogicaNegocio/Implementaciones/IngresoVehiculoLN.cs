using SistemaParqueos.Dominio.DTO;
using SistemaParqueos.Dominio.Entidades;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;

namespace SistemaParqueos.LogicaNegocio.Implementaciones;

public class IngresoVehiculoLN : IIngresoVehiculoLN
{
    private readonly IIngresoVehiculoAD
        _ingresoVehiculoAD;

    public IngresoVehiculoLN(
        IIngresoVehiculoAD ingresoVehiculoAD)
    {
        _ingresoVehiculoAD = ingresoVehiculoAD;
    }

    public async Task<List<IngresoVehiculoDTO>>
        ObtenerTodosAsync()
    {
        List<IngresoVehiculo> ingresos =
            await _ingresoVehiculoAD.ObtenerTodosAsync();

        return ingresos
            .Select(ingreso => MapearADTO(ingreso))
            .ToList();
    }

    public async Task<IngresoVehiculoDTO?>
        ObtenerPorIdAsync(int id)
    {
        IngresoVehiculo? ingreso =
            await _ingresoVehiculoAD
                .ObtenerPorIdAsync(id);

        return ingreso is null
            ? null
            : MapearADTO(ingreso);
    }

    public async Task<IngresoVehiculoDTO>
        RegistrarIngresoAsync(
            IngresoVehiculoDTO ingresoDTO)
    {
        bool vehiculoExiste =
            await _ingresoVehiculoAD
                .ExisteVehiculoActivoAsync(
                    ingresoDTO.VehiculoId
                );

        if (!vehiculoExiste)
        {
            throw new InvalidOperationException(
                "El vehículo seleccionado no existe o está inactivo."
            );
        }

        bool tieneIngresoActivo =
            await _ingresoVehiculoAD
                .VehiculoTieneIngresoActivoAsync(
                    ingresoDTO.VehiculoId
                );

        if (tieneIngresoActivo)
        {
            throw new InvalidOperationException(
                "El vehículo ya tiene un ingreso activo."
            );
        }

        EspacioParqueo? espacio =
            await _ingresoVehiculoAD
                .ObtenerEspacioDisponibleAsync(
                    ingresoDTO.ParqueoId
                );

        if (espacio is null)
        {
            throw new InvalidOperationException(
                "No hay espacios disponibles en el parqueo seleccionado."
            );
        }

        DateTime fechaActual =
     TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
         DateTime.UtcNow,
         "Central America Standard Time"
     );

        espacio.Disponible = false;
        espacio.ActualizadoEn = fechaActual;
        espacio.ActualizadoPor = "Sistema";

        IngresoVehiculo ingreso = new()
        {
            VehiculoId = ingresoDTO.VehiculoId,
            EspacioId = espacio.EspacioId,
            FechaIngreso = fechaActual,
            FechaSalida = null,
            Estado = "Activo",
            CreadoEn = fechaActual,
            CreadoPor = "Sistema"
        };
        IngresoVehiculo creado =
            await _ingresoVehiculoAD.CrearAsync(ingreso);

        return MapearADTO(
            creado,
            espacio.ParqueoId
        );
    }

    public async Task<IngresoVehiculoDTO?>
        RegistrarSalidaAsync(int id)
    {
        IngresoVehiculo? ingreso =
            await _ingresoVehiculoAD
                .ObtenerPorIdAsync(id);

        if (ingreso is null)
        {
            return null;
        }

        if (ingreso.FechaSalida is not null ||
            ingreso.Estado != "Activo")
        {
            throw new InvalidOperationException(
                "El ingreso ya fue finalizado."
            );
        }
        DateTime fechaSalida =
    TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
        DateTime.UtcNow,
        "Central America Standard Time"
    );

        if (fechaSalida < ingreso.FechaIngreso)
        {
            throw new ArgumentException(
                "La fecha de salida no puede ser anterior a la fecha de ingreso."
            );
        }

        bool actualizado =
            await _ingresoVehiculoAD
                .RegistrarSalidaAsync(
                    id,
                    fechaSalida
                );

        if (!actualizado)
        {
            throw new InvalidOperationException(
                "No se pudo registrar la salida."
            );
        }

        IngresoVehiculo? actualizadoIngreso =
            await _ingresoVehiculoAD
                .ObtenerPorIdAsync(id);

        return actualizadoIngreso is null
            ? null
            : MapearADTO(actualizadoIngreso);
    }

    private static IngresoVehiculoDTO MapearADTO(
        IngresoVehiculo ingreso,
        int? parqueoId = null)
    {
        return new IngresoVehiculoDTO
        {
            IngresoId = ingreso.IngresoId,
            VehiculoId = ingreso.VehiculoId,
            ParqueoId = parqueoId ??
                ingreso.Espacio?.ParqueoId ?? 0,
            EspacioId = ingreso.EspacioId,
            FechaIngreso = ingreso.FechaIngreso,
            FechaSalida = ingreso.FechaSalida,
            Estado = ingreso.Estado
        };
    }
}
