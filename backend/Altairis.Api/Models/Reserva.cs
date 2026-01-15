using System.ComponentModel.DataAnnotations;

namespace Altairis.Api.Models;

public class Reserva
{
  [Key]
  public int Id { get; set; }

  [Required]
  public int HotelId { get; set; }

  [Required]
  public int TipoHabitacionId { get; set; }

  // Datos del Cliente
  [Required]
  public string NombreHuesped { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;

  // Fechas de la estancia
  [Required]
  public DateTime FechaEntrada { get; set; }

  [Required]
  public DateTime FechaSalida { get; set; }

  // Fecha en que se hizo la reserva
  public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}