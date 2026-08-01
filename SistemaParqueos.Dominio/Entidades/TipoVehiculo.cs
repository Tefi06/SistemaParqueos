using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class TipoVehiculo
{
    public int TipoVehiculoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual ICollection<Tarifa> Tarifas { get; set; } = new List<Tarifa>();

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
