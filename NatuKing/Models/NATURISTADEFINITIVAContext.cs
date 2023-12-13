using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NatuKing.Models
{
    public partial class NATURISTADEFINITIVAContext : DbContext
    {
        public NATURISTADEFINITIVAContext()
        {
        }

        public NATURISTADEFINITIVAContext(DbContextOptions<NATURISTADEFINITIVAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Commer> Commers { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Compro> Compros { get; set; } = null!;
        public virtual DbSet<Empcom> Empcoms { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Empven> Empvens { get; set; } = null!;
        public virtual DbSet<Mercancium> Mercancia { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Venmer> Venmers { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NPGL15G;DataBase=NATURISTADEFINITIVA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commer>(entity =>
            {
                entity.HasKey(e => e.Idcommer)
                    .HasName("PK__COMMER__D0F8E35BEB676526");

                entity.ToTable("COMMER");

                entity.Property(e => e.Idcommer).HasColumnName("IDCOMMER");

                entity.Property(e => e.Cantidadcm)
                    .HasColumnName("CANTIDADCM")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Clvcom).HasColumnName("CLVCOM");

                entity.Property(e => e.Clvmer).HasColumnName("CLVMER");

                entity.HasOne(d => d.ClvcomNavigation)
                    .WithMany(p => p.Commers)
                    .HasForeignKey(d => d.Clvcom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COMMER__CLVCOM__5AEE82B9");

                entity.HasOne(d => d.ClvmerNavigation)
                    .WithMany(p => p.Commers)
                    .HasForeignKey(d => d.Clvmer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COMMER__CLVMER__5BE2A6F2");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.Clvcom)
                    .HasName("PK__COMPRAS__DE76D5EC0304BBF0");

                entity.ToTable("COMPRAS");

                entity.Property(e => e.Clvcom)
                    .ValueGeneratedNever()
                    .HasColumnName("CLVCOM");

                entity.Property(e => e.Fechac)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FECHAC");

                entity.Property(e => e.Totalc)
                    .HasColumnName("TOTALC")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Compro>(entity =>
            {
                entity.HasKey(e => e.Idcompro)
                    .HasName("PK__COMPRO__DC4C1204FF69E1B8");

                entity.ToTable("COMPRO");

                entity.Property(e => e.Idcompro).HasColumnName("IDCOMPRO");

                entity.Property(e => e.Clvcom).HasColumnName("CLVCOM");

                entity.Property(e => e.Clvpro).HasColumnName("CLVPRO");

                entity.HasOne(d => d.ClvcomNavigation)
                    .WithMany(p => p.Compros)
                    .HasForeignKey(d => d.Clvcom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COMPRO__CLVCOM__6477ECF3");

                entity.HasOne(d => d.ClvproNavigation)
                    .WithMany(p => p.Compros)
                    .HasForeignKey(d => d.Clvpro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COMPRO__CLVPRO__656C112C");
            });

            modelBuilder.Entity<Empcom>(entity =>
            {
                entity.HasKey(e => e.Idempcom)
                    .HasName("PK__EMPCOM__0516DD99433EB23B");

                entity.ToTable("EMPCOM");

                entity.Property(e => e.Idempcom).HasColumnName("IDEMPCOM");

                entity.Property(e => e.Clvcom).HasColumnName("CLVCOM");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.ClvcomNavigation)
                    .WithMany(p => p.Empcoms)
                    .HasForeignKey(d => d.Clvcom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPCOM__CLVCOM__571DF1D5");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Empcoms)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPCOM__ID__5629CD9C");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("EMPLEADO");

                entity.HasIndex(e => e.Telefonoe, "UQ__EMPLEADO__49C2C273BC9EE160")
                    .IsUnique();

                entity.HasIndex(e => e.Nombree, "UQ__EMPLEADO__EA6368AA2D7C1216")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Correoe)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CORREOE");

                entity.Property(e => e.Habilitado).HasColumnName("HABILITADO");

                entity.Property(e => e.Horario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("HORARIO");

                entity.Property(e => e.Nombree)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NOMBREE");

                entity.Property(e => e.Ppass)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PPASS");

                entity.Property(e => e.Puesto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PUESTO");

                entity.Property(e => e.Sueldo).HasColumnName("SUELDO");

                entity.Property(e => e.Telefonoe)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONOE");
            });

            modelBuilder.Entity<Empven>(entity =>
            {
                entity.HasKey(e => e.Idempven)
                    .HasName("PK__EMPVEN__3F59DF9AACABA634");

                entity.ToTable("EMPVEN");

                entity.Property(e => e.Idempven).HasColumnName("IDEMPVEN");

                entity.Property(e => e.Clvvent).HasColumnName("CLVVENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.ClvventNavigation)
                    .WithMany(p => p.Empvens)
                    .HasForeignKey(d => d.Clvvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPVEN__CLVVENT__534D60F1");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Empvens)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPVEN__ID__52593CB8");
            });

            modelBuilder.Entity<Mercancium>(entity =>
            {
                entity.HasKey(e => e.Clvmer)
                    .HasName("PK__MERCANCI__B012133E333F997F");

                entity.ToTable("MERCANCIA");

                entity.HasIndex(e => e.Nombrem, "UQ__MERCANCI__EA6368A284D25201")
                    .IsUnique();

                entity.Property(e => e.Clvmer)
                    .ValueGeneratedNever()
                    .HasColumnName("CLVMER");

                entity.Property(e => e.Caducidad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CADUCIDAD");

                entity.Property(e => e.Codbar)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("CODBAR");

                entity.Property(e => e.Cura)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CURA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Existencia)
                    .HasColumnName("EXISTENCIA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Foto)
                    .IsUnicode(false)
                    .HasColumnName("FOTO");

                entity.Property(e => e.Habilitado).HasColumnName("HABILITADO");

                entity.Property(e => e.Nombrem)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NOMBREM");

                entity.Property(e => e.Precomp)
                    .HasColumnName("PRECOMP")
                    .HasDefaultValueSql("((0.0))");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRESENTACION");

                entity.Property(e => e.Prevent)
                    .HasColumnName("PREVENT")
                    .HasDefaultValueSql("((0.0))");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Clvpro)
                    .HasName("PK__PROVEEDO__DDB01047B5B873F5");

                entity.ToTable("PROVEEDOR");

                entity.HasIndex(e => e.Telefonop, "UQ__PROVEEDO__49C2C2042AA6BC2A")
                    .IsUnique();

                entity.HasIndex(e => e.Correop, "UQ__PROVEEDO__6B57B58AF6DB45F4")
                    .IsUnique();

                entity.HasIndex(e => e.Nombrep, "UQ__PROVEEDO__EA6368A7FA434D24")
                    .IsUnique();

                entity.Property(e => e.Clvpro)
                    .ValueGeneratedNever()
                    .HasColumnName("CLVPRO");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CIUDAD");

                entity.Property(e => e.Correop)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CORREOP");

                entity.Property(e => e.Cp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CP");

                entity.Property(e => e.Habilitado).HasColumnName("HABILITADO");

                entity.Property(e => e.Nombrep)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NOMBREP");

                entity.Property(e => e.Telefonop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONOP");
            });

            modelBuilder.Entity<Venmer>(entity =>
            {
                entity.HasKey(e => e.Idvenmer)
                    .HasName("PK__VENMER__4895D1FB06B07D68");

                entity.ToTable("VENMER");

                entity.Property(e => e.Idvenmer).HasColumnName("IDVENMER");

                entity.Property(e => e.Cantidadvm)
                    .HasColumnName("CANTIDADVM")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Clvmer).HasColumnName("CLVMER");

                entity.Property(e => e.Clvvent).HasColumnName("CLVVENT");

                entity.HasOne(d => d.ClvmerNavigation)
                    .WithMany(p => p.Venmers)
                    .HasForeignKey(d => d.Clvmer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VENMER__CLVMER__5FB337D6");

                entity.HasOne(d => d.ClvventNavigation)
                    .WithMany(p => p.Venmers)
                    .HasForeignKey(d => d.Clvvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VENMER__CLVVENT__60A75C0F");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.Clvvent)
                    .HasName("PK__VENTAS__F09B525C43A873F0");

                entity.ToTable("VENTAS");

                entity.Property(e => e.Clvvent)
                    .ValueGeneratedNever()
                    .HasColumnName("CLVVENT");

                entity.Property(e => e.Fechav)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FECHAV");

                entity.Property(e => e.Totalv)
                    .HasColumnName("TOTALV")
                    .HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
