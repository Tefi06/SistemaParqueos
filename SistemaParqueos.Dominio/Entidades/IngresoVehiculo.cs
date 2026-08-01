using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class IngresoVehiculo
{
    public int IngresoId { get; set; }

    public int VehiculoId { get; set; }

    public int EspacioId { get; set; }

    public DateTime FechaIngreso { get; set; }

    public DateTime? FechaSalida { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual EspacioParqueo Espacio { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
