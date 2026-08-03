using Microsoft.EntityFrameworkCore;
using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.AccesoDatos.Contexto;

public partial class ParqueosContext
{
    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    partial void OnModelCreatingPartial(
        ModelBuilder modelBuilder
    )
    {
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId);

            entity.ToTable("Roles");

            entity.HasIndex(
                e => e.Nombre,
                "UQ_Roles_Nombre"
            ).IsUnique();

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);

            entity.ToTable("Usuarios");

            entity.HasIndex(
                e => e.Correo,
                "UQ_Usuarios_Correo"
            ).IsUnique();

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(e => e.ClaveHash)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql(
                    "(sysdatetime())"
                );

            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(e => e.Rol)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(e => e.RolId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName(
                    "FK_Usuarios_Roles"
                );
        });
    }
}