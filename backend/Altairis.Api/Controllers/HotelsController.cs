using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Altairis.Api.Data;
using Altairis.Api.Models;

namespace Altairis.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public HotelsController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET: api/hotels
  // Devuelve la lista completa de hoteles
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
  {
    return await _context.Hoteles.ToListAsync();
  }

  // GET: api/hotels/5
  // Busca un hotel por su ID
  [HttpGet("{id}")]
  public async Task<ActionResult<Hotel>> GetHotel(int id)
  {
    var hotel = await _context.Hoteles.FindAsync(id);

    if (hotel == null)
    {
      return NotFound();
    }

    return hotel;
  }

  // POST: api/hotels
  // Crea un nuevo hotel en la base de datos
  [HttpPost]
  public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
  {
    // 1. Añadimos el hotel al contexto
    _context.Hoteles.Add(hotel);

    // 2. Guardamos los cambios en la base de datos (Async)
    await _context.SaveChangesAsync();

    // 3. Devolvemos el hotel creado con su nuevo ID
    return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
  }
}