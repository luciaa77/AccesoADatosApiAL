using AccesoADatosApi2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
namespace AccesoADatosApi2.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<Personaje> Personajes => Set<Personaje>();
    public DbSet<Guerrero> Guerreros => Set<Guerrero>();
    public DbSet<Mago> Magos => Set<Mago>();
    public DbSet<Arquero> Arqueros => Set<Arquero>();
    public DbSet<Clerigo> Clerigos => Set<Clerigo>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // (BORRA la línea UseTptMappingStrategy)

        var converter = new ValueConverter<JsonDocument, string>(
        v => v.RootElement.GetRawText(),
        v => JsonDocument.Parse(v, default(JsonDocumentOptions))
    );

    var comparer = new ValueComparer<JsonDocument>(
        (l, r) => l.RootElement.GetRawText() == r.RootElement.GetRawText(),
        v => v.RootElement.GetRawText().GetHashCode(),
        v => JsonDocument.Parse(v.RootElement.GetRawText(), default(JsonDocumentOptions))
    );

    modelBuilder.Entity<Personaje>()
        .Property(p => p.Rasgos)
        .HasConversion(converter)
        .Metadata.SetValueComparer(comparer);
    }

}
