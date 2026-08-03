using System;

namespace SistemaParqueos.Dominio.Entidades;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual Rol Rol { get; set; } = null!;
}