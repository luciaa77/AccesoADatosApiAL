using AccesoADatosApi2.Data;
using AccesoADatosApi2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<GameDbContext>(opt =>
{
    var cs = builder.Configuration.GetConnectionString("GameDb");
    opt.UseNpgsql(cs);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Ok("Funciona"))
   .WithName("Health");

// CRUD POLIMÓRFICO

// GET: todos
app.MapGet("/api/personajes", async (GameDbContext db) =>
{
    var lista = await db.Personajes.AsNoTracking().ToListAsync();
    return Results.Ok(lista);
})
.WithName("GetAllPersonajes")
.Produces(200);

// GET: por id
app.MapGet("/api/personajes/{id:int}", async (int id, GameDbContext db) =>
{
    var pj = await db.Personajes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    return pj is null ? Results.NotFound() : Results.Ok(pj);
})
.WithName("GetPersonajeById")
.Produces(200)
.Produces(404);

// POST: Guerrero
app.MapPost("/api/personajes/guerreros", async (Guerrero g, GameDbContext db) =>
{
    if (g.FechaCreacion == default) g.FechaCreacion = DateTime.UtcNow;
    db.Guerreros.Add(g);
    await db.SaveChangesAsync();
    return Results.Created($"/api/personajes/{g.Id}", g);
})
.WithName("CreateGuerrero")
.Accepts<Guerrero>("application/json")
.Produces<Guerrero>(201);

// POST: Mago
app.MapPost("/api/personajes/magos", async (Mago m, GameDbContext db) =>
{
    if (m.FechaCreacion == default) m.FechaCreacion = DateTime.UtcNow;
    db.Magos.Add(m);
    await db.SaveChangesAsync();
    return Results.Created($"/api/personajes/{m.Id}", m);
})
.WithName("CreateMago")
.Accepts<Mago>("application/json")
.Produces<Mago>(201);

// POST: Arquero
app.MapPost("/api/personajes/arqueros", async (Arquero a, GameDbContext db) =>
{
    if (a.FechaCreacion == default) a.FechaCreacion = DateTime.UtcNow;
    db.Arqueros.Add(a);
    await db.SaveChangesAsync();
    return Results.Created($"/api/personajes/{a.Id}", a);
})
.WithName("CreateArquero")
.Accepts<Arquero>("application/json")
.Produces<Arquero>(201);

// POST: Clerigo
app.MapPost("/api/personajes/clerigos", async (Clerigo c, GameDbContext db) =>
{
    if (c.FechaCreacion == default) c.FechaCreacion = DateTime.UtcNow;
    db.Clerigos.Add(c);
    await db.SaveChangesAsync();
    return Results.Created($"/api/personajes/{c.Id}", c);
})
.WithName("CreateClerigo")
.Accepts<Clerigo>("application/json")
.Produces<Clerigo>(201);

// PUT: Guerrero
app.MapPut("/api/personajes/guerreros/{id:int}", async (int id, Guerrero body, GameDbContext db) =>
{
    var g = await db.Guerreros.FirstOrDefaultAsync(x => x.Id == id);
    if (g is null) return Results.NotFound();

    g.Nombre = body.Nombre;
    g.Nivel = body.Nivel;
    g.Gremio = body.Gremio;
    g.ArmaPrincipal = body.ArmaPrincipal;
    g.Furia = body.Furia;
    g.Rasgos = body.Rasgos;

    await db.SaveChangesAsync();
    return Results.Ok(g);
})
.WithName("UpdateGuerrero")
.Accepts<Guerrero>("application/json")
.Produces<Guerrero>(200)
.Produces(404);

// PUT: Mago
app.MapPut("/api/personajes/magos/{id:int}", async (int id, Mago body, GameDbContext db) =>
{
    var m = await db.Magos.FirstOrDefaultAsync(x => x.Id == id);
    if (m is null) return Results.NotFound();

    m.Nombre = body.Nombre;
    m.Nivel = body.Nivel;
    m.Gremio = body.Gremio;
    m.Mana = body.Mana;
    m.ElementoPrincipal = body.ElementoPrincipal;
    m.Rasgos = body.Rasgos;

    await db.SaveChangesAsync();
    return Results.Ok(m);
})
.WithName("UpdateMago")
.Accepts<Mago>("application/json")
.Produces<Mago>(200)
.Produces(404);

// PUT: Arquero
app.MapPut("/api/personajes/arqueros/{id:int}", async (int id, Arquero body, GameDbContext db) =>
{
    var a = await db.Arqueros.FirstOrDefaultAsync(x => x.Id == id);
    if (a is null) return Results.NotFound();

    a.Nombre = body.Nombre;
    a.Nivel = body.Nivel;
    a.Gremio = body.Gremio;
    a.Precision = body.Precision;
    a.TieneMascota = body.TieneMascota;
    a.Rasgos = body.Rasgos;

    await db.SaveChangesAsync();
    return Results.Ok(a);
})
.WithName("UpdateArquero")
.Accepts<Arquero>("application/json")
.Produces<Arquero>(200)
.Produces(404);

// PUT: Clerigo
app.MapPut("/api/personajes/clerigos/{id:int}", async (int id, Clerigo body, GameDbContext db) =>
{
    var c = await db.Clerigos.FirstOrDefaultAsync(x => x.Id == id);
    if (c is null) return Results.NotFound();

    c.Nombre = body.Nombre;
    c.Nivel = body.Nivel;
    c.Gremio = body.Gremio;
    c.Deidad = body.Deidad;
    c.PuntosSanacion = body.PuntosSanacion;
    c.Rasgos = body.Rasgos;

    await db.SaveChangesAsync();
    return Results.Ok(c);
})
.WithName("UpdateClerigo")
.Accepts<Clerigo>("application/json")
.Produces<Clerigo>(200)
.Produces(404);

// DELETE
app.MapDelete("/api/personajes/{id:int}", async (int id, GameDbContext db) =>
{
    var pj = await db.Personajes.FirstOrDefaultAsync(p => p.Id == id);
    if (pj is null) return Results.NotFound();

    db.Personajes.Remove(pj);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeletePersonaje")
.Produces(204)
.Produces(404);


//CONSULTAS COMPLEJAS (2)

// 1) Filtrado profundo por JSON: que exista la clave "MiedoA"
// IMPORTANTE: en TPT no se puede usar FromSqlRaw sobre db.Personajes.
// Lo hacemos sobre la tabla concreta (Arqueros) y usando JsonExists.
app.MapGet("/api/consultas/con-miedo", async (GameDbContext db) =>
{
    var res = await db.Arqueros
        .Where(a => a.Rasgos != null && EF.Functions.JsonExists(a.Rasgos, "MiedoA"))
        .AsNoTracking()
        .ToListAsync();

    return Results.Ok(res);
})
.WithName("ConsultaConMiedo")
.Produces(200);

// 2) Agrupación polimórfica: cantidad por tipo + media de nivel
app.MapGet("/api/consultas/resumen-por-tipo", async (GameDbContext db) =>
{
    var res = await db.Personajes
        .AsNoTracking()
        .GroupBy(p =>
            p is Guerrero ? "Guerrero" :
            p is Mago ? "Mago" :
            p is Arquero ? "Arquero" :
            p is Clerigo ? "Clerigo" : "Otro")
        .Select(g => new
        {
            Tipo = g.Key,
            Cantidad = g.Count(),
            MediaNivel = g.Average(x => x.Nivel)
        })
        .ToListAsync();

    return Results.Ok(res);
})
.WithName("ResumenPorTipo")
.Produces(200);

app.Run();