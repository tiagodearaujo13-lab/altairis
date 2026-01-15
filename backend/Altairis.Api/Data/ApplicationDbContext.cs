using Microsoft.EntityFrameworkCore;
using Altairis.Api.Models;

namespace Altairis.Api.Data;

/// <summary>
/// Contexto de la base de datos para la aplicación Altairis.
/// Maneja la comunicación entre el código C# y PostgreSQL.
/// </summary>
public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  // Definición de las tablas del sistema
  public DbSet<Hotel> Hoteles { get; set; }

  public DbSet<TipoHabitacion> TiposHabitacion { get; set; }

  public DbSet<Disponibilidad> Disponibilidades { get; set; }

  public DbSet<Reserva> Reservas { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Aquí podemos añadir configuraciones específicas si el cliente lo requiere
    // Ejemplo: Índices para búsquedas rápidas en grandes volúmenes de datos
    modelBuilder.Entity<Hotel>().HasIndex(h => h.Nombre);
  }
}