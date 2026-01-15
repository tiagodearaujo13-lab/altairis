using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Altairis.Api.Data;
using Altairis.Api.Models;

namespace Altairis.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservasController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public ReservasController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET: api/Reservas
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
  {
    return await _context.Reservas.ToListAsync();
  }

  // GET: api/Reservas/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Reserva>> GetReserva(int id)
  {
    var reserva = await _context.Reservas.FindAsync(id);
    if (reserva == null) return NotFound();
    return reserva;
  }

  // POST: api/Reservas
  // Este é o método MÁGICO que valida e desconta o stock!
  [HttpPost]
  public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
  {
    // 1. Validação básica de datas
    if (reserva.FechaEntrada >= reserva.FechaSalida)
    {
      return BadRequest("La fecha de salida debe ser posterior a la de entrada.");
    }

    // 2. Calcular os dias da estadia (ex: dia 20 e dia 21)
    // Nota: Não contamos o dia de saída porque o hóspede sai de manhã.
    var diasNecessarios = new List<DateTime>();
    for (var data = reserva.FechaEntrada.Date; data < reserva.FechaSalida.Date; data = data.AddDays(1))
    {
      diasNecessarios.Add(data);
    }

    // 3. Buscar disponibilidade no banco para esses dias
    var disponibilidades = await _context.Disponibilidades
        .Where(d => d.HotelId == reserva.HotelId
                 && d.TipoHabitacionId == reserva.TipoHabitacionId
                 && d.Fecha >= reserva.FechaEntrada.Date
                 && d.Fecha < reserva.FechaSalida.Date)
        .ToListAsync();

    // 4. Verificar se há stock para TODOS os dias
    foreach (var dia in diasNecessarios)
    {
      var disponibilidadeDia = disponibilidades.FirstOrDefault(d => d.Fecha.Date == dia);

      // Se não existe registo ou a quantidade é 0, rejeitamos a reserva
      if (disponibilidadeDia == null || disponibilidadeDia.Cantidad <= 0)
      {
        return BadRequest($"No hay disponibilidad para el día {dia:dd/MM/yyyy}.");
      }

      // 5. Descontar do Stock (Inventário)
      disponibilidadeDia.Cantidad -= 1;
    }

    // 6. Se chegámos aqui, está tudo livre! Gravamos a reserva e atualizamos o stock.
    _context.Reservas.Add(reserva);

    // O SaveChanges salva tanto a Reserva como a atualização da Disponibilidade numa transação única.
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
  }
}