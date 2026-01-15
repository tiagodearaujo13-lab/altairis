using System.ComponentModel.DataAnnotations;

namespace Altairis.Api.Models;

/// <summary>
/// Entidad que representa un Hotel en el sistema de Altairis.
/// </summary>
public class Hotel
{
  [Key]
  public int Id { get; set; }

  [Required]
  [StringLength(100)]
  public string Nombre { get; set; } = string.Empty;

  [Required]
  public string Direccion { get; set; } = string.Empty;

  [Required]
  public string Ciudad { get; set; } = string.Empty;

  // Clasificación por estrellas (1-5)
  public int Estrellas { get; set; }

  // Indica si el hotel está activo en la plataforma
  public bool EstaActivo { get; set; } = true;

  // Fecha de registro en el sistema
  public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}