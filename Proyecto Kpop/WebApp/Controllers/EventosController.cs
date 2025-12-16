using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;
using SistemaFanclub.Models;

namespace SistemaFanclub.Controllers
{
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string zona, DateTime? fecha)
        {
            ViewData["SearchString"] = searchString;
            ViewData["Zona"] = zona;
            ViewData["Fecha"] = fecha;

            var eventos = _context.Eventos
                .Include(e => e.Idol)
                .Include(e => e.Reservas)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                eventos = eventos.Where(e =>
                    e.Titulo.Contains(searchString) ||
                    (e.Descripcion != null && e.Descripcion.Contains(searchString)) ||
                    (e.TipoEvento != null && e.TipoEvento.Contains(searchString))
                );
            }

            if (!string.IsNullOrEmpty(zona))
            {
                eventos = eventos.Where(e => e.Zona.Contains(zona));
            }

            if (fecha.HasValue)
            {
                eventos = eventos.Where(e => e.FechaEvento.Date == fecha.Value.Date);
            }

            eventos = eventos.Where(e => e.Estado != "Finalizado" && e.Estado != "Cancelado");

            return View(await eventos
                .OrderBy(e => e.FechaEvento)
                .ThenBy(e => e.HoraInicio)
                .ToListAsync());
        }

        public async Task<IActionResult> Proximos()
        {
            var eventos = await _context.Eventos
                .Include(e => e.Idol)
                .Include(e => e.Reservas)
                .Where(e => e.FechaEvento >= DateTime.Today && e.Estado == "Programado")
                .OrderBy(e => e.FechaEvento)
                .ThenBy(e => e.HoraInicio)
                .ToListAsync();

            return View(eventos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Idol)
                .Include(e => e.CreadoPor)
                .Include(e => e.Reservas.Where(r => r.FechaCancelacion == null))
                    .ThenInclude(r => r.Miembro)
                .FirstOrDefaultAsync(m => m.EventoId == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        public IActionResult Create()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (!usuarioId.HasValue)
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para crear eventos";
                return RedirectToAction("Login", "Autenticacion");
            }

            ViewData["IdolId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico");
            
            var evento = new Evento
            {
                FechaEvento = DateTime.Today.AddDays(7),
                HoraInicio = new TimeSpan(14, 0, 0),
                RequiereReserva = true,
                Estado = "Programado"
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descripcion,IdolId,TipoEvento,FechaEvento,HoraInicio,HoraFin,Zona,Direccion,DireccionDetallada,Capacidad,RequiereReserva,ImagenURL")] Evento evento)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (!usuarioId.HasValue)
            {
                return RedirectToAction("Login", "Autenticacion");
            }

            if (ModelState.IsValid)
            {
                evento.PuestosDisponibles = evento.Capacidad;
                evento.Estado = "Programado";
                evento.CreadoPorUsuarioId = usuarioId.Value;
                evento.FechaCreacion = DateTime.Now;
                evento.UltimaActualizacion = DateTime.Now;

                _context.Add(evento);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Evento creado exitosamente";
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdolId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico", evento.IdolId);
            return View(evento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            ViewData["IdolId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico", evento.IdolId);
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Titulo,Descripcion,IdolId,TipoEvento,FechaEvento,HoraInicio,HoraFin,Zona,Direccion,DireccionDetallada,Capacidad,PuestosDisponibles,RequiereReserva,Estado,ImagenURL,CreadoPorUsuarioId,FechaCreacion")] Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    evento.UltimaActualizacion = DateTime.Now;
                    _context.Update(evento);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Evento actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdolId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico", evento.IdolId);
            return View(evento);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Idol)
                .Include(e => e.CreadoPor)
                .FirstOrDefaultAsync(m => m.EventoId == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Evento eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reservar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Idol)
                .FirstOrDefaultAsync(e => e.EventoId == id);

            if (evento == null)
            {
                return NotFound();
            }

            if (evento.PuestosDisponibles <= 0)
            {
                TempData["ErrorMessage"] = "No hay puestos disponibles para este evento";
                return RedirectToAction(nameof(Details), new { id });
            }

            // Prellenar con datos del usuario si está logueado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var reserva = new Reserva
            {
                EventoId = evento.EventoId,
                NumeroPersonas = 1
            };

            if (usuarioId.HasValue)
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
                if (usuario != null)
                {
                    reserva.UsuarioId = usuario.UsuarioId;
                    reserva.NombreReserva = usuario.NombreCompleto;
                    reserva.EmailContacto = usuario.Email;
                }
            }

            ViewBag.Evento = evento;
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservar([Bind("EventoId,MiembroId,NombreReserva,EmailContacto,TelefonoContacto,NumeroPersonas,Notas")] Reserva reserva)
        {
            var evento = await _context.Eventos.FindAsync(reserva.EventoId);
            if (evento == null)
            {
                return NotFound();
            }

            if (evento.PuestosDisponibles < reserva.NumeroPersonas)
            {
                ModelState.AddModelError("NumeroPersonas", $"Solo hay {evento.PuestosDisponibles} puestos disponibles");
            }

            if (ModelState.IsValid)
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                reserva.UsuarioId = usuarioId;
                reserva.Confirmada = true;
                reserva.FechaReserva = DateTime.Now;

                evento.PuestosDisponibles -= reserva.NumeroPersonas;
                evento.UltimaActualizacion = DateTime.Now;

                _context.Add(reserva);
                _context.Update(evento);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"¡Reserva confirmada! Código de reserva: #{reserva.ReservaId}";
                return RedirectToAction(nameof(Details), new { id = reserva.EventoId });
            }

            ViewBag.Evento = evento;
            return View(reserva);
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
