using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaParqueos.Dominio.DTO;

public class TipoVehiculoDTO
{
    public int TipoVehiculoId { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public bool Activo { get; set; }
}
