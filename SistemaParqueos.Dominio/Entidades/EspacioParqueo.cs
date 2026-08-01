using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class EspacioParqueo
{
    public int EspacioId { get; set; }

    public int ParqueoId { get; set; }

    public string NumeroEspacio { get; set; } = null!;

    public bool Disponible { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual ICollection<IngresoVehiculo> IngresoVehiculos { get; set; } = new List<IngresoVehiculo>();

    public virtual Parqueo Parqueo { get; set; } = null!;
}
