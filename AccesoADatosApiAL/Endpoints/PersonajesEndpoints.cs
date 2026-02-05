// =======================================================
// Autor: Persona B - API/Endpoints/Consultas/Docs
// Proyecto: D&DSoft MMORPG Backend
// Archivo: Endpoints/PersonajesEndpoints.cs
// =======================================================

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace AccesoADatosApiAL.Endpoints; // <- si tu proyecto tiene otro namespace, ajusta SOLO esta línea

public static class PersonajesEndpoints
{
    public static RouteGroupBuilder MapPersonajesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/personajes")
            .WithTags("Personajes");

        // GET ALL (polimórfico)
        group.MapGet("/", async Task<Ok<List<object>>> (AppDbContext db) =>
        {
            var personajes = await db.Personajes.AsNoTracking().ToListAsync();
            return TypedResults.Ok(personajes.Select(ToApiDto).ToList());
        });

        // GET BY ID
        group.MapGet("/{id:int}", async Task<Results<Ok<object>, NotFound>> (int id, AppDbContext db) =>
        {
            var p = await db.Personajes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return p is null ? TypedResults.NotFound() : TypedResults.Ok(ToApiDto(p));
        })
        .WithName("GetPersonajeById");

        // POST por tipo
        group.MapPost("/guerreros", async Task<Results<CreatedAtRoute<object>, ValidationProblem>> (CreateGuerreroRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = new Guerrero
            {
                Nombre = req.Nombre!,
                Nivel = req.Nivel,
                FechaCreacion = req.FechaCreacion,
                Gremio = req.Gremio,
                Rasgos = req.Rasgos,
                ArmaPrincipal = req.ArmaPrincipal!,
                Furia = req.Furia
            };

            db.Guerreros.Add(entity);
            await db.SaveChangesAsync();

            return TypedResults.CreatedAtRoute(ToApiDto(entity), "GetPersonajeById", new { id = entity.Id });
        });

        group.MapPost("/magos", async Task<Results<CreatedAtRoute<object>, ValidationProblem>> (CreateMagoRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = new Mago
            {
                Nombre = req.Nombre!,
                Nivel = req.Nivel,
                FechaCreacion = req.FechaCreacion,
                Gremio = req.Gremio,
                Rasgos = req.Rasgos,
                Mana = req.Mana,
                ElementoPrincipal = req.ElementoPrincipal!
            };

            db.Magos.Add(entity);
            await db.SaveChangesAsync();

            return TypedResults.CreatedAtRoute(ToApiDto(entity), "GetPersonajeById", new { id = entity.Id });
        });

        group.MapPost("/arqueros", async Task<Results<CreatedAtRoute<object>, ValidationProblem>> (CreateArqueroRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = new Arquero
            {
                Nombre = req.Nombre!,
                Nivel = req.Nivel,
                FechaCreacion = req.FechaCreacion,
                Gremio = req.Gremio,
                Rasgos = req.Rasgos,
                Precision = req.Precision,
                TieneMascota = req.TieneMascota
            };

            db.Arqueros.Add(entity);
            await db.SaveChangesAsync();

            return TypedResults.CreatedAtRoute(ToApiDto(entity), "GetPersonajeById", new { id = entity.Id });
        });

        group.MapPost("/clerigos", async Task<Results<CreatedAtRoute<object>, ValidationProblem>> (CreateClerigoRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = new Clerigo
            {
                Nombre = req.Nombre!,
                Nivel = req.Nivel,
                FechaCreacion = req.FechaCreacion,
                Gremio = req.Gremio,
                Rasgos = req.Rasgos,
                Deidad = req.Deidad!,
                PuntosSanacion = req.PuntosSanacion
            };

            db.Clerigos.Add(entity);
            await db.SaveChangesAsync();

            return TypedResults.CreatedAtRoute(ToApiDto(entity), "GetPersonajeById", new { id = entity.Id });
        });

        // PUT por tipo (incluye Rasgos)
        group.MapPut("/guerreros/{id:int}", async Task<Results<Ok<object>, NotFound, ValidationProblem>> (int id, UpdateGuerreroRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = await db.Guerreros.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return TypedResults.NotFound();

            ApplyCommon(entity, req.Nombre!, req.Nivel, req.FechaCreacion, req.Gremio, req.Rasgos);
            entity.ArmaPrincipal = req.ArmaPrincipal!;
            entity.Furia = req.Furia;

            await db.SaveChangesAsync();
            return TypedResults.Ok(ToApiDto(entity));
        });

        group.MapPut("/magos/{id:int}", async Task<Results<Ok<object>, NotFound, ValidationProblem>> (int id, UpdateMagoRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = await db.Magos.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return TypedResults.NotFound();

            ApplyCommon(entity, req.Nombre!, req.Nivel, req.FechaCreacion, req.Gremio, req.Rasgos);
            entity.Mana = req.Mana;
            entity.ElementoPrincipal = req.ElementoPrincipal!;

            await db.SaveChangesAsync();
            return TypedResults.Ok(ToApiDto(entity));
        });

        group.MapPut("/arqueros/{id:int}", async Task<Results<Ok<object>, NotFound, ValidationProblem>> (int id, UpdateArqueroRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = await db.Arqueros.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return TypedResults.NotFound();

            ApplyCommon(entity, req.Nombre!, req.Nivel, req.FechaCreacion, req.Gremio, req.Rasgos);
            entity.Precision = req.Precision;
            entity.TieneMascota = req.TieneMascota;

            await db.SaveChangesAsync();
            return TypedResults.Ok(ToApiDto(entity));
        });

        group.MapPut("/clerigos/{id:int}", async Task<Results<Ok<object>, NotFound, ValidationProblem>> (int id, UpdateClerigoRequest req, AppDbContext db) =>
        {
            var errors = Validate(req);
            if (errors is not null) return TypedResults.ValidationProblem(errors);

            var entity = await db.Clerigos.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return TypedResults.NotFound();

            ApplyCommon(entity, req.Nombre!, req.Nivel, req.FechaCreacion, req.Gremio, req.Rasgos);
            entity.Deidad = req.Deidad!;
            entity.PuntosSanacion = req.PuntosSanacion;

            await db.SaveChangesAsync();
            return TypedResults.Ok(ToApiDto(entity));
        });

        // DELETE
        group.MapDelete("/{id:int}", async Task<Results<NoContent, NotFound>> (int id, AppDbContext db) =>
        {
            var entity = await db.Personajes.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return TypedResults.NotFound();

            db.Personajes.Remove(entity);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        });

        // -------------------------
        // Avanzado 1: JSON -> tengan "MiedoA"
        // -------------------------
        group.MapGet("/avanzado/con-miedo", async Task<Ok<List<object>>> (AppDbContext db) =>
        {
            var personajes = await db.Personajes
                .AsNoTracking()
                .Where(p => EF.Functions.JsonExists(p.Rasgos!, "MiedoA"))
                .ToListAsync();

            return TypedResults.Ok(personajes.Select(ToApiDto).ToList());
        });

        // -------------------------
        // Avanzado 2: resumen polimórfico (count + media nivel)
        // -------------------------
        group.MapGet("/avanzado/resumen-por-tipo", async Task<Ok<object>> (AppDbContext db) =>
        {
            var resumen = new
            {
                guerreros = new { count = await db.Guerreros.CountAsync(), avgNivel = await db.Guerreros.Select(x => (double)x.Nivel).DefaultIfEmpty(0).AverageAsync() },
                magos = new { count = await db.Magos.CountAsync(), avgNivel = await db.Magos.Select(x => (double)x.Nivel).DefaultIfEmpty(0).AverageAsync() },
                arqueros = new { count = await db.Arqueros.CountAsync(), avgNivel = await db.Arqueros.Select(x => (double)x.Nivel).DefaultIfEmpty(0).AverageAsync() },
                clerigos = new { count = await db.Clerigos.CountAsync(), avgNivel = await db.Clerigos.Select(x => (double)x.Nivel).DefaultIfEmpty(0).AverageAsync() },
            };

            return TypedResults.Ok(resumen);
        });

        return group;
    }

    // Helpers
    private static void ApplyCommon(Personaje p, string nombre, int nivel, DateTime fechaCreacion, string? gremio, JsonDocument? rasgos)
    {
        p.Nombre = nombre;
        p.Nivel = nivel;
        p.FechaCreacion = fechaCreacion;
        p.Gremio = gremio;
        p.Rasgos = rasgos;
    }

    private static object ToApiDto(Personaje p) =>
        p switch
        {
            Guerrero g => new { id = g.Id, tipo = "Guerrero", nombre = g.Nombre, nivel = g.Nivel, fechaCreacion = g.FechaCreacion, gremio = g.Gremio, rasgos = g.Rasgos, armaPrincipal = g.ArmaPrincipal, furia = g.Furia },
            Mago m => new { id = m.Id, tipo = "Mago", nombre = m.Nombre, nivel = m.Nivel, fechaCreacion = m.FechaCreacion, gremio = m.Gremio, rasgos = m.Rasgos, mana = m.Mana, elementoPrincipal = m.ElementoPrincipal },
            Arquero a => new { id = a.Id, tipo = "Arquero", nombre = a.Nombre, nivel = a.Nivel, fechaCreacion = a.FechaCreacion, gremio = a.Gremio, rasgos = a.Rasgos, precision = a.Precision, tieneMascota = a.TieneMascota },
            Clerigo c => new { id = c.Id, tipo = "Clerigo", nombre = c.Nombre, nivel = c.Nivel, fechaCreacion = c.FechaCreacion, gremio = c.Gremio, rasgos = c.Rasgos, deidad = c.Deidad, puntosSanacion = c.PuntosSanacion },
            _ => new { id = p.Id, tipo = "Personaje", nombre = p.Nombre, nivel = p.Nivel, fechaCreacion = p.FechaCreacion, gremio = p.Gremio, rasgos = p.Rasgos }
        };

    private static Dictionary<string, string[]>? Validate(object model)
    {
        var ctx = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var ok = Validator.TryValidateObject(model, ctx, results, true);
        if (ok) return null;

        var dict = new Dictionary<string, string[]>();
        foreach (var r in results)
        {
            var key = r.MemberNames.FirstOrDefault() ?? "";
            dict[key] = dict.TryGetValue(key, out var old)
                ? [.. old, r.ErrorMessage ?? "Error de validación"]
                : [r.ErrorMessage ?? "Error de validación"];
        }
        return dict;
    }

    // Request DTOs (solo API)
    public sealed record CreateGuerreroRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, [Required] string? ArmaPrincipal, int Furia);
    public sealed record CreateMagoRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, int Mana, [Required] string? ElementoPrincipal);
    public sealed record CreateArqueroRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, double Precision, bool TieneMascota);
    public sealed record CreateClerigoRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, [Required] string? Deidad, int PuntosSanacion);

    public sealed record UpdateGuerreroRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, [Required] string? ArmaPrincipal, int Furia);
    public sealed record UpdateMagoRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, int Mana, [Required] string? ElementoPrincipal);
    public sealed record UpdateArqueroRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, double Precision, bool TieneMascota);
    public sealed record UpdateClerigoRequest([Required, MaxLength(50)] string? Nombre, [Range(1, 100)] int Nivel, DateTime FechaCreacion, string? Gremio, JsonDocument? Rasgos, [Required] string? Deidad, int PuntosSanacion);
}
