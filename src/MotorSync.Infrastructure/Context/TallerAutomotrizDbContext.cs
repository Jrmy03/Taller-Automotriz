using Microsoft.EntityFrameworkCore;
using MotorSync.Domain.Constants;
using MotorSync.Domain.Entities;

namespace MotorSync.Infrastructure.Context;

/// <summary>
/// DbContext principal de MotorSync reflejando el esquema de la base de datos TallerAutomotriz.
/// Configurado con convenciones Fluent API precisas para SQL Server.
/// </summary>
public class TallerAutomotrizDbContext : DbContext
{
    public TallerAutomotrizDbContext(DbContextOptions<TallerAutomotrizDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    public DbSet<Mecanico> Mecanicos => Set<Mecanico>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<Repuesto> Repuestos => Set<Repuesto>();
    public DbSet<ProveedorRepuesto> ProveedorRepuestos => Set<ProveedorRepuesto>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<OrdenTrabajo> OrdenesTrabajo => Set<OrdenTrabajo>();
    public DbSet<DetalleOt> DetallesOt => Set<DetalleOt>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<Pago> Pagos => Set<Pago>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. CLIENTE
        modelBuilder.Entity<Cliente>(b =>
        {
            b.ToTable("CLIENTE");
            b.HasKey(c => c.IdCliente);
            b.Property(c => c.IdCliente).HasColumnName("id_cliente");
            b.Property(c => c.Identificacion).HasColumnName("identificacion").HasMaxLength(30).IsRequired();
            b.HasIndex(c => c.Identificacion).IsUnique();
            b.Property(c => c.NombreCompleto).HasColumnName("nombre_completo").HasMaxLength(150).IsRequired();
            b.Property(c => c.Telefono).HasColumnName("telefono").HasMaxLength(20);
            b.Property(c => c.Correo).HasColumnName("correo").HasMaxLength(150);
            b.Property(c => c.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoRegistro.Activo);
            b.Ignore(c => c.EstaActivo);
        });

        // 2. MECANICO
        modelBuilder.Entity<Mecanico>(b =>
        {
            b.ToTable("MECANICO");
            b.HasKey(m => m.IdMecanico);
            b.Property(m => m.IdMecanico).HasColumnName("id_mecanico");
            b.Property(m => m.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            b.Property(m => m.Telefono).HasColumnName("telefono").HasMaxLength(20);
            b.Property(m => m.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoRegistro.Activo);

            // Relación muchos a muchos con ESPECIALIDAD (tabla MECANICO_ESPECIALIDAD)
            b.HasMany(m => m.Especialidades)
                .WithMany(e => e.Mecanicos)
                .UsingEntity<Dictionary<string, object>>(
                    "MECANICO_ESPECIALIDAD",
                    r => r.HasOne<Especialidad>().WithMany().HasForeignKey("id_especialidad").HasConstraintName("FK_ME_ESPECIALIDAD"),
                    l => l.HasOne<Mecanico>().WithMany().HasForeignKey("id_mecanico").HasConstraintName("FK_ME_MECANICO"),
                    je =>
                    {
                        je.ToTable("MECANICO_ESPECIALIDAD");
                        je.HasKey("id_mecanico", "id_especialidad").HasName("PK_MECANICO_ESPECIALIDAD");
                    });
        });

        // 3. ESPECIALIDAD
        modelBuilder.Entity<Especialidad>(b =>
        {
            b.ToTable("ESPECIALIDAD");
            b.HasKey(e => e.IdEspecialidad);
            b.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");
            b.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            b.HasIndex(e => e.Nombre).IsUnique();
            b.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(250);
        });

        // 4. PROVEEDOR
        modelBuilder.Entity<Proveedor>(b =>
        {
            b.ToTable("PROVEEDOR");
            b.HasKey(p => p.IdProveedor);
            b.Property(p => p.IdProveedor).HasColumnName("id_proveedor");
            b.Property(p => p.RazonSocial).HasColumnName("razon_social").HasMaxLength(150).IsRequired();
            b.Property(p => p.IdentificacionJuridica).HasColumnName("identificacion_juridica").HasMaxLength(30);
            b.HasIndex(p => p.IdentificacionJuridica).IsUnique().HasFilter("[identificacion_juridica] IS NOT NULL");
            b.Property(p => p.Telefono).HasColumnName("telefono").HasMaxLength(20);
            b.Property(p => p.Correo).HasColumnName("correo").HasMaxLength(150);
            b.Property(p => p.Direccion).HasColumnName("direccion").HasMaxLength(250);
            b.Property(p => p.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoRegistro.Activo);
        });

        // 5. SERVICIO
        modelBuilder.Entity<Servicio>(b =>
        {
            b.ToTable("SERVICIO");
            b.HasKey(s => s.IdServicio);
            b.Property(s => s.IdServicio).HasColumnName("id_servicio");
            b.Property(s => s.Codigo).HasColumnName("codigo").HasMaxLength(30).IsRequired();
            b.HasIndex(s => s.Codigo).IsUnique();
            b.Property(s => s.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            b.Property(s => s.Descripcion).HasColumnName("descripcion").HasMaxLength(250);
            b.Property(s => s.CostoManoObra).HasColumnName("costo_mano_obra").HasPrecision(12, 2).IsRequired();
            b.Property(s => s.TiempoEstimado).HasColumnName("tiempo_estimado");
            b.Property(s => s.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoRegistro.Activo);
        });

        // 6. REPUESTO
        modelBuilder.Entity<Repuesto>(b =>
        {
            b.ToTable("REPUESTO");
            b.HasKey(r => r.IdRepuesto);
            b.Property(r => r.IdRepuesto).HasColumnName("id_repuesto");
            b.Property(r => r.CodigoParte).HasColumnName("codigo_parte").HasMaxLength(50);
            b.HasIndex(r => r.CodigoParte).IsUnique().HasFilter("[codigo_parte] IS NOT NULL");
            b.Property(r => r.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            b.Property(r => r.Categoria).HasColumnName("categoria").HasMaxLength(80);
            b.Property(r => r.CostoCompra).HasColumnName("costo_compra").HasPrecision(12, 2).IsRequired();
            b.Property(r => r.PrecioVenta).HasColumnName("precio_venta").HasPrecision(12, 2).IsRequired();
            b.Property(r => r.StockActual).HasColumnName("stock_actual").HasDefaultValue(0);
            b.Property(r => r.StockMinimo).HasColumnName("stock_minimo").HasDefaultValue(0);
            b.Property(r => r.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoRegistro.Activo);
            b.Ignore(r => r.RequiereReabastecimiento);
        });

        // 7. VEHICULO
        modelBuilder.Entity<Vehiculo>(b =>
        {
            b.ToTable("VEHICULO");
            b.HasKey(v => v.IdVehiculo);
            b.Property(v => v.IdVehiculo).HasColumnName("id_vehiculo");
            b.Property(v => v.IdCliente).HasColumnName("id_cliente").IsRequired();
            b.Property(v => v.Placa).HasColumnName("placa").HasMaxLength(20).IsRequired();
            b.HasIndex(v => v.Placa).IsUnique();
            b.Property(v => v.Marca).HasColumnName("marca").HasMaxLength(50).IsRequired();
            b.Property(v => v.Modelo).HasColumnName("modelo").HasMaxLength(50).IsRequired();
            b.Property(v => v.Anio).HasColumnName("anio");
            b.Property(v => v.Vin).HasColumnName("vin").HasMaxLength(50);
            b.HasIndex(v => v.Vin).IsUnique().HasFilter("[vin] IS NOT NULL");
            b.Property(v => v.Kilometraje).HasColumnName("kilometraje").HasDefaultValue(0);
            b.Property(v => v.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoRegistro.Activo);

            b.HasOne(v => v.Cliente)
                .WithMany(c => c.Vehiculos)
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_VEHICULO_CLIENTE");
        });

        // 8. CITA
        modelBuilder.Entity<Cita>(b =>
        {
            b.ToTable("CITA");
            b.HasKey(c => c.IdCita);
            b.Property(c => c.IdCita).HasColumnName("id_cita");
            b.Property(c => c.IdCliente).HasColumnName("id_cliente").IsRequired();
            b.Property(c => c.IdVehiculo).HasColumnName("id_vehiculo").IsRequired();
            b.Property(c => c.Fecha).HasColumnName("fecha").HasColumnType("date").IsRequired();
            b.Property(c => c.Hora).HasColumnName("hora").HasColumnType("time(0)").IsRequired();
            b.Property(c => c.Motivo).HasColumnName("motivo").HasMaxLength(250);
            b.Property(c => c.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoCita.Pendiente);

            b.HasOne(c => c.Cliente)
                .WithMany(cl => cl.Citas)
                .HasForeignKey(c => c.IdCliente)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CITA_CLIENTE");

            b.HasOne(c => c.Vehiculo)
                .WithMany(v => v.Citas)
                .HasForeignKey(c => c.IdVehiculo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CITA_VEHICULO");
        });

        // 9. PROVEEDOR_REPUESTO
        modelBuilder.Entity<ProveedorRepuesto>(b =>
        {
            b.ToTable("PROVEEDOR_REPUESTO");
            b.HasKey(pr => new { pr.IdProveedor, pr.IdRepuesto }).HasName("PK_PROVEEDOR_REPUESTO");
            b.Property(pr => pr.IdProveedor).HasColumnName("id_proveedor");
            b.Property(pr => pr.IdRepuesto).HasColumnName("id_repuesto");
            b.Property(pr => pr.CostoAdquisicion).HasColumnName("costo_adquisicion").HasPrecision(12, 2).IsRequired();

            b.HasOne(pr => pr.Proveedor)
                .WithMany(p => p.ProveedorRepuestos)
                .HasForeignKey(pr => pr.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_PR_PROVEEDOR");

            b.HasOne(pr => pr.Repuesto)
                .WithMany(r => r.ProveedorRepuestos)
                .HasForeignKey(pr => pr.IdRepuesto)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_PR_REPUESTO");
        });

        // 10. ORDEN_TRABAJO
        modelBuilder.Entity<OrdenTrabajo>(b =>
        {
            b.ToTable("ORDEN_TRABAJO");
            b.HasKey(ot => ot.IdOt);
            b.Property(ot => ot.IdOt).HasColumnName("id_ot");
            b.Property(ot => ot.IdVehiculo).HasColumnName("id_vehiculo").IsRequired();
            b.Property(ot => ot.IdMecanico).HasColumnName("id_mecanico").IsRequired();
            b.Property(ot => ot.IdCita).HasColumnName("id_cita");
            b.Property(ot => ot.FechaApertura).HasColumnName("fecha_apertura").HasColumnType("datetime2(0)").HasDefaultValueSql("SYSDATETIME()");
            b.Property(ot => ot.FechaCierre).HasColumnName("fecha_cierre").HasColumnType("datetime2(0)");
            b.Property(ot => ot.KilometrajeEntrada).HasColumnName("kilometraje_entrada").IsRequired();
            b.Property(ot => ot.MotivoIngreso).HasColumnName("motivo_ingreso").HasMaxLength(250).IsRequired();
            b.Property(ot => ot.Estado).HasColumnName("estado").HasMaxLength(30).HasDefaultValue(EstadoOrdenTrabajo.Pendiente);

            b.HasIndex(ot => ot.IdCita).IsUnique().HasFilter("[id_cita] IS NOT NULL");

            b.HasOne(ot => ot.Vehiculo)
                .WithMany(v => v.OrdenesTrabajo)
                .HasForeignKey(ot => ot.IdVehiculo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OT_VEHICULO");

            b.HasOne(ot => ot.Mecanico)
                .WithMany(m => m.OrdenesTrabajo)
                .HasForeignKey(ot => ot.IdMecanico)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OT_MECANICO");

            b.HasOne(ot => ot.Cita)
                .WithOne(c => c.OrdenTrabajo)
                .HasForeignKey<OrdenTrabajo>(ot => ot.IdCita)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OT_CITA");
        });

        // 11. DETALLE_OT
        modelBuilder.Entity<DetalleOt>(b =>
        {
            b.ToTable("DETALLE_OT");
            b.HasKey(d => d.IdDetalle);
            b.Property(d => d.IdDetalle).HasColumnName("id_detalle");
            b.Property(d => d.IdOt).HasColumnName("id_ot").IsRequired();
            b.Property(d => d.IdServicio).HasColumnName("id_servicio");
            b.Property(d => d.IdRepuesto).HasColumnName("id_repuesto");
            b.Property(d => d.Tipo).HasColumnName("tipo").HasMaxLength(20).IsRequired();
            b.Property(d => d.Cantidad).HasColumnName("cantidad").IsRequired();
            b.Property(d => d.PrecioUnitario).HasColumnName("precio_unitario").HasPrecision(12, 2).IsRequired();
            b.Ignore(d => d.Subtotal);

            b.HasOne(d => d.OrdenTrabajo)
                .WithMany(ot => ot.Detalles)
                .HasForeignKey(d => d.IdOt)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DETALLE_OT");

            b.HasOne(d => d.Servicio)
                .WithMany(s => s.DetallesOt)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DETALLE_SERVICIO");

            b.HasOne(d => d.Repuesto)
                .WithMany(r => r.DetallesOt)
                .HasForeignKey(d => d.IdRepuesto)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DETALLE_REPUESTO");
        });

        // 12. FACTURA
        modelBuilder.Entity<Factura>(b =>
        {
            b.ToTable("FACTURA");
            b.HasKey(f => f.IdFactura);
            b.Property(f => f.IdFactura).HasColumnName("id_factura");
            b.Property(f => f.IdOt).HasColumnName("id_ot").IsRequired();
            b.HasIndex(f => f.IdOt).IsUnique();
            b.Property(f => f.NumeroFactura).HasColumnName("numero_factura").HasMaxLength(40).IsRequired();
            b.HasIndex(f => f.NumeroFactura).IsUnique();
            b.Property(f => f.Fecha).HasColumnName("fecha").HasColumnType("datetime2(0)").HasDefaultValueSql("SYSDATETIME()");
            b.Property(f => f.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2).IsRequired();
            b.Property(f => f.Impuesto).HasColumnName("impuesto").HasPrecision(12, 2).HasDefaultValue(0);
            b.Property(f => f.Total).HasColumnName("total").HasPrecision(12, 2).IsRequired();
            b.Property(f => f.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoFactura.Pendiente);
            b.Ignore(f => f.TotalPagado);
            b.Ignore(f => f.SaldoPendiente);

            b.HasOne(f => f.OrdenTrabajo)
                .WithOne(ot => ot.Factura)
                .HasForeignKey<Factura>(f => f.IdOt)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_FACTURA_OT");
        });

        // 13. PAGO
        modelBuilder.Entity<Pago>(b =>
        {
            b.ToTable("PAGO");
            b.HasKey(p => p.IdPago);
            b.Property(p => p.IdPago).HasColumnName("id_pago");
            b.Property(p => p.IdFactura).HasColumnName("id_factura").IsRequired();
            b.Property(p => p.FechaPago).HasColumnName("fecha_pago").HasColumnType("datetime2(0)").HasDefaultValueSql("SYSDATETIME()");
            b.Property(p => p.Monto).HasColumnName("monto").HasPrecision(12, 2).IsRequired();
            b.Property(p => p.MetodoPago).HasColumnName("metodo_pago").HasMaxLength(30).IsRequired();
            b.Property(p => p.Estado).HasColumnName("estado").HasMaxLength(20).HasDefaultValue(EstadoPago.Registrado);

            b.HasOne(p => p.Factura)
                .WithMany(f => f.Pagos)
                .HasForeignKey(p => p.IdFactura)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_PAGO_FACTURA");
        });
    }
}
