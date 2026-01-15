using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Altairis.Api.Models;

/// <summary>
/// Representa un tipo de habitación (ej: Doble, Suite) asociado a un hotel.
/// </summary>
public class TipoHabitacion
{
  [Key]
  public int Id { get; set; }

  [Required]
  public int HotelId { get; set; } // La clave foránea que conecta con el Hotel

  [Required]
  [StringLength(50)]
  public string Nombre { get; set; } = string.Empty; // Ej: "Suite Presidencial"

  [Required]
  public int Capacidad { get; set; } // Cuántas personas caben

  [Column(TypeName = "decimal(18,2)")]
  public decimal PrecioBase { get; set; }

  // Relación de navegación (para que EF sepa conectar las tablas)
  [JsonIgnore] // Evita ciclos infinitos al convertir a JSON
  public Hotel? Hotel { get; set; }
}