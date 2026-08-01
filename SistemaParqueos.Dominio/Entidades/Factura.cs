using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class Factura
{
    public int FacturaId { get; set; }

    public int IngresoId { get; set; }

    public DateTime FechaFactura { get; set; }

    public decimal HorasCobradas { get; set; }

    public decimal MontoTotal { get; set; }

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual IngresoVehiculo Ingreso { get; set; } = null!;
}
