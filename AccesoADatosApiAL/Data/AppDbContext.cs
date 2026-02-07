// Autor: (Tu nombre) - D&DSoft
using AccesoDatosApiAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatosApiAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Personaje> Personajes { get; set; } = null!;
        public DbSet<Guerrero> Guerreros { get; set; } = null!;
        public DbSet<Mago> Magos { get; set; } = null!;
        public DbSet<Arquero> Arqueros { get; set; } = null!;
        public DbSet<Clerigo> Clerigos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dndsoft");

            modelBuilder.Entity<Personaje>().ToTable("Personajes");
            modelBuilder.Entity<Guerrero>().ToTable("Guerreros");
            modelBuilder.Entity<Mago>().ToTable("Magos");
            modelBuilder.Entity<Arquero>().ToTable("Arqueros");
            modelBuilder.Entity<Clerigo>().ToTable("Clerigos");

            modelBuilder.Entity<Personaje>()
                .Property(p => p.Rasgos)
                .HasColumnType("jsonb");

            base.OnModelCreating(modelBuilder);
        }
    }
}
