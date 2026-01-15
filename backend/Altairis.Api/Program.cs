using Altairis.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------------------------
// 1. Configuración de Servicios (La "Mise-en-place")
// --------------------------------------------------------------------------

// Añadir soporte para Controladores (API)
builder.Services.AddControllers();

// Configuración de Swagger (Documentación automática)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CONFIGURACIÓN DE BASE DE DATOS (PostgreSQL) ---
// Leemos la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Si no hay conexión configurada (ej: al iniciar), usamos una por defecto para que no falle
if (string.IsNullOrEmpty(connectionString))
{
  // Esto es solo para prevenir errores antes de configurar el appsettings.json
  connectionString = "Host=localhost;Database=altairis_db;Username=admin;Password=password123";
}

// Registramos el contexto de datos para que use PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
// ---------------------------------------------------
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend",
      policy =>
      {
        policy.WithOrigins("http://localhost:3000") // A porta do Next.js
                .AllowAnyHeader()
                .AllowAnyMethod();
      });
});

var app = builder.Build();

// --------------------------------------------------------------------------
// 2. Configuración del Pipeline HTTP (Cómo se sirven los platos)
// --------------------------------------------------------------------------

// En desarrollo, mostramos la documentación Swagger
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Redirección HTTPS
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

// Arrancar la aplicación
app.Run();