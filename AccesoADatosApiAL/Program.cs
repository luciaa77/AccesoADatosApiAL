using AccesoDatosApiAI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (PostgreSQL + EF Core)
var connectionString = builder.Configuration.GetConnectionString("GameDb");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger((Action<Swashbuckle.AspNetCore.Swagger.SwaggerOptions>?)null);
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "API DnDSoft OK");

app.Run();
