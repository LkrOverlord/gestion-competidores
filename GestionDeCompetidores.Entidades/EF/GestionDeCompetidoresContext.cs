using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionDeCompetidores.Entidades.EF;

public partial class GestionDeCompetidoresContext : DbContext
{
    public GestionDeCompetidoresContext()
    {
    }

    public GestionDeCompetidoresContext(DbContextOptions<GestionDeCompetidoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competidor> Competidors { get; set; }

    public virtual DbSet<Deporte> Deportes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-UBK79II\\SQLEXPRESS;Database=GestionDeCompetidores;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competidor>(entity =>
        {
            entity.HasKey(e => e.IdCompetidor);

            entity.ToTable("Competidor");

            entity.Property(e => e.IdCompetidor).HasColumnName("idCompetidor");
            entity.Property(e => e.IdDeporte).HasColumnName("idDeporte");
            entity.Property(e => e.NombreCompetidor)
                .HasMaxLength(50)
                .HasColumnName("nombreCompetidor");

            entity.HasOne(d => d.IdDeporteNavigation).WithMany(p => p.Competidors)
                .HasForeignKey(d => d.IdDeporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Competidor_Deporte");
        });

        modelBuilder.Entity<Deporte>(entity =>
        {
            entity.HasKey(e => e.IdDeporte);

            entity.ToTable("Deporte");

            entity.Property(e => e.IdDeporte).HasColumnName("idDeporte");
            entity.Property(e => e.NombreDeporte)
                .HasMaxLength(50)
                .HasColumnName("nombreDeporte");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
