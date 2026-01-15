using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Altairis.Api.Data;
using Altairis.Api.Models;

namespace Altairis.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiposHabitacionController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public TiposHabitacionController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET: api/TiposHabitacion
  // Devuelve todos los tipos de habitación de todos los hoteles
  [HttpGet]
  public async Task<ActionResult<IEnumerable<TipoHabitacion>>> GetTiposHabitacion()
  {
    return await _context.TiposHabitacion.ToListAsync();
  }

  // GET: api/TiposHabitacion/hotel/5
  // FILTRO: Devuelve solo los tipos de habitación de un hotel específico
  [HttpGet("hotel/{hotelId}")]
  public async Task<ActionResult<IEnumerable<TipoHabitacion>>> GetPorHotel(int hotelId)
  {
    var tipos = await _context.TiposHabitacion
                              .Where(t => t.HotelId == hotelId)
                              .ToListAsync();

    if (tipos == null || !tipos.Any())
    {
      return NotFound("No se encontraron habitaciones para este hotel.");
    }

    return tipos;
  }

  // POST: api/TiposHabitacion
  // Crea un nuevo tipo de habitación
  [HttpPost]
  public async Task<ActionResult<TipoHabitacion>> PostTipoHabitacion(TipoHabitacion tipoHabitacion)
  {
    // Validar si el hotel existe antes de crear la habitación
    var hotelExiste = await _context.Hoteles.AnyAsync(h => h.Id == tipoHabitacion.HotelId);
    if (!hotelExiste)
    {
      return BadRequest("El Hotel ID especificado no existe.");
    }

    _context.TiposHabitacion.Add(tipoHabitacion);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetTiposHabitacion), new { id = tipoHabitacion.Id }, tipoHabitacion);
  }
}