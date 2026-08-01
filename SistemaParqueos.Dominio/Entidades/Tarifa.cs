using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class Tarifa
{
    public int TarifaId { get; set; }

    public int TipoVehiculoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal MontoHora { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual TipoVehiculo TipoVehiculo { get; set; } = null!;
}
