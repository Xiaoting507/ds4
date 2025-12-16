using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Models;

namespace SistemaFanclub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Idol> Idols { get; set; }
        public DbSet<Miembro> Miembros { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("LL_Usuarios");
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Rol).IsRequired().HasMaxLength(50).HasDefaultValue("Miembro");
                entity.Property(e => e.Activo).HasDefaultValue(true);
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuración Agencia
            modelBuilder.Entity<Agencia>(entity =>
            {
                entity.ToTable("LL_Agencias");
                entity.HasKey(e => e.AgenciaId);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Pais).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Activa).HasDefaultValue(true);
            });

            // Configuración Idol
            modelBuilder.Entity<Idol>(entity =>
            {
                entity.ToTable("LL_Idols");
                entity.HasKey(e => e.IdolId);
                entity.Property(e => e.NombreArtistico).IsRequired().HasMaxLength(200);
                entity.Property(e => e.TipoIdol).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Activo).HasDefaultValue(true);

                entity.HasOne(e => e.Agencia)
                    .WithMany(a => a.Idols)
                    .HasForeignKey(e => e.AgenciaId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración Miembro
            modelBuilder.Entity<Miembro>(entity =>
            {
                entity.ToTable("LL_Miembros");
                entity.HasKey(e => e.MiembroId);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Activo).HasDefaultValue(true);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Miembros)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.IdolFavorito)
                    .WithMany(i => i.MiembrosFans)
                    .HasForeignKey(e => e.IdolFavoritoId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración Evento
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.ToTable("LL_Eventos");
                entity.HasKey(e => e.EventoId);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Zona).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Capacidad).HasDefaultValue(0);
                entity.Property(e => e.PuestosDisponibles).HasDefaultValue(0);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(50).HasDefaultValue("Programado");
                entity.Property(e => e.RequiereReserva).HasDefaultValue(true);

                entity.HasOne(e => e.Idol)
                    .WithMany(i => i.Eventos)
                    .HasForeignKey(e => e.IdolId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.CreadoPor)
                    .WithMany(u => u.EventosCreados)
                    .HasForeignKey(e => e.CreadoPorUsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración Reserva
            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.ToTable("LL_Reservas");
                entity.HasKey(e => e.ReservaId);
                entity.Property(e => e.NombreReserva).IsRequired().HasMaxLength(200);
                entity.Property(e => e.EmailContacto).IsRequired().HasMaxLength(150);
                entity.Property(e => e.NumeroPersonas).HasDefaultValue(1);
                entity.Property(e => e.Confirmada).HasDefaultValue(false);

                entity.HasOne(e => e.Evento)
                    .WithMany(ev => ev.Reservas)
                    .HasForeignKey(e => e.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Miembro)
                    .WithMany(m => m.Reservas)
                    .HasForeignKey(e => e.MiembroId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Reservas)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}