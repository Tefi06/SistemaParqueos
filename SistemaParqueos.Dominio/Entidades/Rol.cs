using System.Collections.Generic;

namespace SistemaParqueos.Dominio.Entidades;

public partial class Rol
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; }
        = new List<Usuario>();
}