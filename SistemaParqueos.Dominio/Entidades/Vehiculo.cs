using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }

    public int ClienteId { get; set; }

    public int TipoVehiculoId { get; set; }

    public string Placa { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string? Modelo { get; set; }

    public string? Color { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<IngresoVehiculo> IngresoVehiculos { get; set; } = new List<IngresoVehiculo>();

    public virtual TipoVehiculo TipoVehiculo { get; set; } = null!;
}
