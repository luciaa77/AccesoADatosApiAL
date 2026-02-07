# D&DSoft - API (Minimal API + EF Core 9 + PostgreSQL)

Proyecto listo para **Code-First** con **TPT** (Table Per Type) y campo **jsonb** para `Rasgos`.

## 1) Configura la conexión (appsettings.json)

En `appsettings.json`:

```json
"ConnectionStrings": {
  "GameDb": "Host=localhost;Port=5432;Database=ddsoft_db;Username=ddsoft_user;Password=ddsoft_pass;Search Path=ddsoft"
}
```

- Si tu contenedor expone otro puerto, cambia `Port=...`
- Si NO usas schema, quita `Search Path=ddsoft`.

## 2) Crear BD desde el código (migraciones)

En la carpeta del proyecto:

```powershell
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

## 3) Swagger

Abre:
- `http://localhost:5044/swagger`

> Si te da **address already in use**, es que ya tienes la API levantada en ese puerto. Para la anterior ejecución (terminal) con `Ctrl + C`.

## 4) Endpoints

Base: `/api/personajes`

### CRUD
- `GET /api/personajes`
- `GET /api/personajes/{id}`
- `POST /api/personajes/guerreros`
- `POST /api/personajes/magos`
- `POST /api/personajes/arqueros`
- `POST /api/personajes/clerigos`
- `PUT /api/personajes/guerreros/{id}`
- `PUT /api/personajes/magos/{id}`
- `PUT /api/personajes/arqueros/{id}`
- `PUT /api/personajes/clerigos/{id}`
- `DELETE /api/personajes/{id}`

### Consultas complejas (3)
- `GET /api/personajes/avanzado/con-miedo`
- `GET /api/personajes/avanzado/resumen-por-tipo`
- `GET /api/personajes/avanzado/clerigos-o-magos-nivel-alto`

## 5) Ejemplos de BODY (para Swagger -> Try it out)

### POST Guerrero
```json
{
  "nombre": "Thorgal",
  "nivel": 10,
  "fechaCreacion": "2026-02-06T19:00:00",
  "gremio": "Lobos",
  "rasgos": { "Cicatrices": ["Ojo derecho"], "Trofeos": 5 },
  "armaPrincipal": "Hacha",
  "furia": 20
}
```

### POST Mago
```json
{
  "nombre": "Merlín",
  "nivel": 70,
  "fechaCreacion": "2026-02-06T19:00:00",
  "gremio": null,
  "rasgos": { "Libro": "Grimorio", "MiedoA": "Fuego" },
  "mana": 300,
  "elementoPrincipal": "Hielo"
}
```
