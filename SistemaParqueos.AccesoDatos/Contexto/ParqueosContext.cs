using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaParqueos.Dominio.Entidades;

namespace SistemaParqueos.AccesoDatos.Contexto;

public partial class ParqueosContext : DbContext
{
    public ParqueosContext(DbContextOptions<ParqueosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<EspacioParqueo> EspacioParqueos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<IngresoVehiculo> IngresoVehiculos { get; set; }

    public virtual DbSet<Parqueo> Parqueos { get; set; }

    public virtual DbSet<Tarifa> Tarifas { get; set; }

    public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Cedula, "UQ_Cliente_Cedula").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.Apellidos).HasMaxLength(150);
            entity.Property(e => e.Cedula).HasMaxLength(25);
            entity.Property(e => e.Correo).HasMaxLength(254);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Telefono).HasMaxLength(25);
        });

        modelBuilder.Entity<EspacioParqueo>(entity =>
        {
            entity.HasKey(e => e.EspacioId);

            entity.ToTable("EspacioParqueo");

            entity.HasIndex(e => new { e.ParqueoId, e.NumeroEspacio }, "UQ_Espacio").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Disponible).HasDefaultValue(true);
            entity.Property(e => e.NumeroEspacio).HasMaxLength(20);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Parqueo).WithMany(p => p.EspacioParqueos)
                .HasForeignKey(d => d.ParqueoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Espacio_Parqueo");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("Factura");

            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.FechaFactura).HasPrecision(3);
            entity.Property(e => e.HorasCobradas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Ingreso).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IngresoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Ingreso");
        });

        modelBuilder.Entity<IngresoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IngresoId);

            entity.ToTable("IngresoVehiculo");

            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaIngreso).HasPrecision(3);
            entity.Property(e => e.FechaSalida).HasPrecision(3);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Espacio).WithMany(p => p.IngresoVehiculos)
                .HasForeignKey(d => d.EspacioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ingreso_Espacio");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.IngresoVehiculos)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ingreso_Vehiculo");
        });

        modelBuilder.Entity<Parqueo>(entity =>
        {
            entity.ToTable("Parqueo");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.NombreParqueo).HasMaxLength(150);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Telefono).HasMaxLength(25);
        });

        modelBuilder.Entity<Tarifa>(entity =>
        {
            entity.ToTable("Tarifa");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.MontoHora).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.TipoVehiculo).WithMany(p => p.Tarifas)
                .HasForeignKey(d => d.TipoVehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarifa_TipoVehiculo");
        });

        modelBuilder.Entity<TipoVehiculo>(entity =>
        {
            entity.ToTable("TipoVehiculo");

            entity.HasIndex(e => e.Descripcion, "UQ_TipoVehiculo").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculo");

            entity.HasIndex(e => e.Placa, "UQ_Vehiculo_Placa").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ActualizadoEn).HasPrecision(3);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CreadoEn)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreadoPor).HasMaxLength(50);
            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.Modelo).HasMaxLength(100);
            entity.Property(e => e.Placa).HasMaxLength(20);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Cliente).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculo_Cliente");

            entity.HasOne(d => d.TipoVehiculo).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.TipoVehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculo_TipoVehiculo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
