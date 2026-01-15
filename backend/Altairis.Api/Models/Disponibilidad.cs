using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Altairis.Api.Models;

/// <summary>
/// Gestiona cuántas habitaciones libres hay por día.
/// Ejemplo: El 20/01/2026 hay 5 habitaciones "Suite" libres.
/// </summary>
public class Disponibilidad
{
  [Key]
  public int Id { get; set; }

  [Required]
  public int HotelId { get; set; }

  [Required]
  public int TipoHabitacionId { get; set; }

  [Required]
  public DateTime Fecha { get; set; }

  [Required]
  public int Cantidad { get; set; } // Inventario disponible (stock)

  // Relaciones para navegar entre datos
  [JsonIgnore]
  public Hotel? Hotel { get; set; }

  [JsonIgnore]
  public TipoHabitacion? TipoHabitacion { get; set; }
}