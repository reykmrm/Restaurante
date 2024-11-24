using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models.Tables;

public partial class RestauranteContext : DbContext
{
    public RestauranteContext()
    {
    }

    public RestauranteContext(DbContextOptions<RestauranteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Mesero> Meseros { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("workstation id=Restaurante-p.mssql.somee.com;packet size=4096;user id=reykmrm1_SQLLogin_2;pwd=tya2lrmaz8;data source=Restaurante-p.mssql.somee.com;persist security info=False;initial catalog=Restaurante-p;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Identificacion).HasName("PK__Cliente__D6F931E401C995E7");

            entity.ToTable("Cliente");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("PK__DetalleF__DB5F4631DA3DCE71");

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.Plato)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdSupervisorNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdSupervisor)
                .HasConstraintName("FK__DetalleFa__IdSup__45F365D3");

            entity.HasOne(d => d.NroFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.NroFactura)
                .HasConstraintName("FK__DetalleFa__NroFa__44FF419A");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.NroFactura).HasName("PK__Factura__54177A8458AA2BC5");

            entity.ToTable("Factura");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Factura__IdClien__403A8C7D");

            entity.HasOne(d => d.IdMeseroNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdMesero)
                .HasConstraintName("FK__Factura__IdMeser__4222D4EF");

            entity.HasOne(d => d.NroMesaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.NroMesa)
                .HasConstraintName("FK__Factura__NroMesa__412EB0B6");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.NroMesa).HasName("PK__Mesa__3F2EFFCF271A3CE2");

            entity.ToTable("Mesa");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Reservada).HasDefaultValue(false);
        });

        modelBuilder.Entity<Mesero>(entity =>
        {
            entity.HasKey(e => e.IdMesero).HasName("PK__Mesero__5C84C5634410E8F1");

            entity.ToTable("Mesero");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.IdSupervisor).HasName("PK__Supervis__F80D52820576F16B");

            entity.ToTable("Supervisor");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
