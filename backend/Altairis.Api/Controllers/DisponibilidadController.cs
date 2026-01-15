using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Altairis.Api.Data;
using Altairis.Api.Models;

namespace Altairis.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DisponibilidadController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public DisponibilidadController(ApplicationDbContext context)
  {
    _context = context;
  }

  // POST: api/Disponibilidad
  // Define o inventário para um dia específico (ex: 10 quartos livres dia 20)
  [HttpPost]
  public async Task<ActionResult<Disponibilidad>> PostDisponibilidad(Disponibilidad disponibilidad)
  {
    // Verifica se já existe registo para este quarto nesta data (para não duplicar)
    var existente = await _context.Disponibilidades
        .FirstOrDefaultAsync(d => d.HotelId == disponibilidad.HotelId &&
                                  d.TipoHabitacionId == disponibilidad.TipoHabitacionId &&
                                  d.Fecha.Date == disponibilidad.Fecha.Date);

    if (existente != null)
    {
      // Se já existe, atualizamos a quantidade em vez de criar novo
      existente.Cantidad = disponibilidad.Cantidad;
      await _context.SaveChangesAsync();
      return Ok(existente);
    }

    _context.Disponibilidades.Add(disponibilidad);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetDisponibilidad), new { id = disponibilidad.Id }, disponibilidad);
  }

  // GET: api/Disponibilidad
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Disponibilidad>>> GetDisponibilidad()
  {
    return await _context.Disponibilidades.ToListAsync();
  }

  // GET: api/Disponibilidad/hotel/1?fechaInicio=2026-01-01&fechaFin=2026-01-31
  // Este é o endpoint que o Frontend vai usar para desenhar o calendário!
  [HttpGet("hotel/{hotelId}")]
  public async Task<ActionResult<IEnumerable<Disponibilidad>>> GetPorRango(int hotelId, DateTime fechaInicio, DateTime fechaFin)
  {
    // Converte as datas para UTC para evitar problemas de fuso horário
    var inicio = DateTime.SpecifyKind(fechaInicio.Date, DateTimeKind.Utc);
    var fin = DateTime.SpecifyKind(fechaFin.Date, DateTimeKind.Utc);

    return await _context.Disponibilidades
        .Where(d => d.HotelId == hotelId && d.Fecha >= inicio && d.Fecha <= fin)
        .OrderBy(d => d.Fecha)
        .ToListAsync();
  }
}