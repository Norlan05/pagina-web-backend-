using CLINICA.Model_request;
using CLINICA.Modelos;
using Microsoft.EntityFrameworkCore;
namespace CLINICA.Data
{
    public partial class ClinicaDbcontext: DbContext
    {
        public ClinicaDbcontext() 
        {
        }
        public ClinicaDbcontext(DbContextOptions<ClinicaDbcontext> options)
            : base(options)
        {
        }
        public virtual DbSet<reservas> Reservas { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<reservas>(entity => 
            {
                entity.ToTable("reservas","dbo");
                entity.Property(e => e.id)
                .HasColumnName("id");
                entity.Property(e => e.nombre)
                .HasColumnName("nombre");
                entity.Property(e => e.apellido)
                .HasColumnName("apellido");
                entity.Property(e => e.correo_electronico)
                .HasColumnName("correo_electronico");
                entity.Property(e => e.numero_telefono)
                .HasColumnName("numero_telefono");
                entity.Property(e => e.fecha)
                .HasColumnName("fecha_hora");
               
            });
            modelBuilder.Entity<usuarios>(entity => 
            {
                entity.ToTable("usuarios", "dbo");
                entity.Property(c => c.id_usuario)
                .HasColumnName("Id");
                entity.Property(c => c.nombre)
                .HasColumnName("Username");
                entity.Property(c => c.email)
                .HasColumnName("Email");
                entity.Property(c => c.password)
                .HasColumnName("Password");
                entity.Property(c => c.created_at)
                .HasColumnName("CreatedAt");
                entity.Property(c => c.ResetToken)
                .HasColumnName("ResetToken");
                entity.Property(c => c.ResetTokenExpiry)
                .HasColumnName("ResetTokenExpiry");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
