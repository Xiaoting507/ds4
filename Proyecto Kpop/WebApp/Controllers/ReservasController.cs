using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;

namespace SistemaFanclub.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string estado)
        {
            ViewData["Estado"] = estado;

            var reservas = _context.Reservas
                .Include(r => r.Evento)
                    .ThenInclude(e => e!.Idol)
                .Include(r => r.Miembro)
                .Include(r => r.Usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
            {
                switch (estado.ToLower())
                {
                    case "confirmadas":
                        reservas = reservas.Where(r => r.Confirmada && r.FechaCancelacion == null);
                        break;
                    case "pendientes":
                        reservas = reservas.Where(r => !r.Confirmada && r.FechaCancelacion == null);
                        break;
                    case "canceladas":
                        reservas = reservas.Where(r => r.FechaCancelacion != null);
                        break;
                    case "asistio":
                        reservas = reservas.Where(r => r.Asistio == true);
                        break;
                }
            }

            return View(await reservas
                .OrderByDescending(r => r.FechaReserva)
                .ToListAsync());
        }

        public async Task<IActionResult> MisReservas()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (!usuarioId.HasValue)
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para ver tus reservas";
                return RedirectToAction("Login", "Autenticacion");
            }

            var reservas = await _context.Reservas
                .Include(r => r.Evento)
                    .ThenInclude(e => e!.Idol)
                .Where(r => r.UsuarioId == usuarioId.Value)
                .OrderByDescending(r => r.FechaReserva)
                .ToListAsync();

            return View(reservas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Evento)
                    .ThenInclude(e => e!.Idol)
                .Include(r => r.Miembro)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmar(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            reserva.Confirmada = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva confirmada exitosamente";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Evento)
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (reserva.FechaCancelacion.HasValue)
            {
                TempData["ErrorMessage"] = "Esta reserva ya está cancelada";
                return RedirectToAction(nameof(Details), new { id });
            }

            reserva.FechaCancelacion = DateTime.Now;

            if (reserva.Evento != null)
            {
                reserva.Evento.PuestosDisponibles += reserva.NumeroPersonas;
                reserva.Evento.UltimaActualizacion = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva cancelada exitosamente. Los puestos han sido liberados.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarAsistencia(int id, bool asistio)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            reserva.Asistio = asistio;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = asistio ? 
                "Asistencia registrada" : 
                "Inasistencia registrada";

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Evento)
                .Include(r => r.Miembro)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Evento)
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva != null)
            {
                // Liberar puestos si no estaba cancelada
                if (!reserva.FechaCancelacion.HasValue && reserva.Evento != null)
                {
                    reserva.Evento.PuestosDisponibles += reserva.NumeroPersonas;
                }

                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Reserva eliminada exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
