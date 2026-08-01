using System;
using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class Parqueo
{
    public int ParqueoId { get; set; }

    public string NombreParqueo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public int CapacidadTotal { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public string? CreadoPor { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public string? ActualizadoPor { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual ICollection<EspacioParqueo> EspacioParqueos { get; set; } = new List<EspacioParqueo>();
}
