using Microsoft.EntityFrameworkCore;
using FutbolPeruano.Models;

namespace FutbolPeruano.Data
{
    public class FutbolPeruanoContext : DbContext
    {
        public FutbolPeruanoContext(DbContextOptions<FutbolPeruanoContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la restricción única para evitar que un jugador esté dos veces en el mismo equipo
            modelBuilder.Entity<Assignment>()
                .HasIndex(a => new { a.PlayerId, a.TeamId })
                .IsUnique();

            // Configurar las relaciones
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Player)
                .WithMany(p => p.Assignments)
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Team)
                .WithMany(t => t.Assignments)
                .HasForeignKey(a => a.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
