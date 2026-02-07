using AccesoADatosApiAL.Endpoints;
using AccesoDatosApiAL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("GameDb");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccesoADatosApiAL v1");
        c.RoutePrefix = "swagger"; // para que sea /swagger
    });
}


// Endpoints
app.MapGet("/", () => "API DnDSoft OK");

app.MapPersonajesEndpoints();

app.Run();
