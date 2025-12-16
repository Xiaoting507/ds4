using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;

namespace SistemaFanclub.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AsistenciaEventos()
        {
            var eventos = await _context.Eventos
                .Include(e => e.Idol)
                .Include(e => e.Reservas)
                .Where(e => e.FechaEvento <= DateTime.Today)
                .OrderByDescending(e => e.FechaEvento)
                .Select(e => new
                {
                    Evento = e,
                    TotalReservas = e.Reservas.Count(r => r.FechaCancelacion == null),
                    Asistencias = e.Reservas.Count(r => r.Asistio == true),
                    Inasistencias = e.Reservas.Count(r => r.Asistio == false),
                    PorcentajeAsistencia = e.Reservas.Count(r => r.FechaCancelacion == null) > 0
                        ? (e.Reservas.Count(r => r.Asistio == true) * 100.0 / e.Reservas.Count(r => r.FechaCancelacion == null))
                        : 0
                })
                .ToListAsync();

            return View(eventos);
        }

        public async Task<IActionResult> EstadisticasMiembros()
        {
            var miembros = await _context.Miembros
                .Include(m => m.IdolFavorito)
                .Include(m => m.Reservas)
                    .ThenInclude(r => r.Evento)
                .Where(m => m.Activo)
                .Select(m => new
                {
                    Miembro = m,
                    TotalReservas = m.Reservas.Count(r => r.FechaCancelacion == null),
                    EventosAsistidos = m.Reservas.Count(r => r.Asistio == true),
                    EventosCancelados = m.Reservas.Count(r => r.FechaCancelacion != null),
                    UltimaAsistencia = m.Reservas
                        .Where(r => r.Asistio == true)
                        .OrderByDescending(r => r.Evento!.FechaEvento)
                        .Select(r => r.Evento!.FechaEvento)
                        .FirstOrDefault()
                })
                .OrderByDescending(m => m.TotalReservas)
                .ToListAsync();

            return View(miembros);
        }

        public async Task<IActionResult> EventosPopulares()
        {
            var eventosPopulares = await _context.Eventos
                .Include(e => e.Idol)
                .Include(e => e.Reservas)
                .Select(e => new
                {
                    Evento = e,
                    TotalReservas = e.Reservas.Count(r => r.FechaCancelacion == null),
                    TotalPersonas = e.Reservas
                        .Where(r => r.FechaCancelacion == null)
                        .Sum(r => r.NumeroPersonas),
                    PorcentajeOcupacion = e.Capacidad > 0
                        ? ((e.Capacidad - e.PuestosDisponibles) * 100.0 / e.Capacidad)
                        : 0,
                    Estado = e.Estado
                })
                .OrderByDescending(e => e.TotalPersonas)
                .Take(20)
                .ToListAsync();

            return View(eventosPopulares);
        }

        public async Task<IActionResult> PorIdol()
        {
            var estadisticasIdols = await _context.Idols
                .Include(i => i.Eventos)
                    .ThenInclude(e => e.Reservas)
                .Include(i => i.MiembrosFans)
                .Where(i => i.Activo)
                .Select(i => new
                {
                    Idol = i,
                    TotalEventos = i.Eventos.Count,
                    EventosRealizados = i.Eventos.Count(e => e.Estado == "Finalizado"),
                    TotalFans = i.MiembrosFans.Count(m => m.Activo),
                    TotalReservas = i.Eventos.SelectMany(e => e.Reservas).Count(r => r.FechaCancelacion == null),
                    ProximoEvento = i.Eventos
                        .Where(e => e.FechaEvento >= DateTime.Today && e.Estado == "Programado")
                        .OrderBy(e => e.FechaEvento)
                        .FirstOrDefault()
                })
                .OrderByDescending(i => i.TotalEventos)
                .ToListAsync();

            return View(estadisticasIdols);
        }

        public async Task<IActionResult> PorZona()
        {
            var estadisticasZonas = await _context.Eventos
                .Include(e => e.Reservas)
                .GroupBy(e => e.Zona)
                .Select(g => new
                {
                    Zona = g.Key,
                    TotalEventos = g.Count(),
                    EventosRealizados = g.Count(e => e.Estado == "Finalizado"),
                    EventosProximos = g.Count(e => e.FechaEvento >= DateTime.Today && e.Estado == "Programado"),
                    TotalReservas = g.SelectMany(e => e.Reservas).Count(r => r.FechaCancelacion == null),
                    CapacidadTotal = g.Sum(e => e.Capacidad),
                    UltimoEvento = g.OrderByDescending(e => e.FechaEvento).FirstOrDefault()
                })
                .OrderByDescending(z => z.TotalEventos)
                .ToListAsync();

            return View(estadisticasZonas);
        }

        public async Task<IActionResult> Dashboard()
        {
            // Estadísticas generales
            ViewBag.TotalIdols = await _context.Idols.Where(i => i.Activo).CountAsync();
            ViewBag.TotalMiembros = await _context.Miembros.Where(m => m.Activo).CountAsync();
            ViewBag.TotalEventos = await _context.Eventos.CountAsync();
            ViewBag.EventosRealizados = await _context.Eventos
                .Where(e => e.Estado == "Finalizado")
                .CountAsync();
            ViewBag.ProximosEventos = await _context.Eventos
                .Where(e => e.FechaEvento >= DateTime.Today && e.Estado == "Programado")
                .CountAsync();
            ViewBag.TotalReservas = await _context.Reservas
                .Where(r => r.FechaCancelacion == null)
                .CountAsync();

            var seisMesesAtras = DateTime.Today.AddMonths(-6);
            var eventosPorMesRaw = await _context.Eventos
                .Where(e => e.FechaEvento >= seisMesesAtras)
                .GroupBy(e => new { e.FechaEvento.Year, e.FechaEvento.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Count()
                })
                .ToListAsync();

            var eventosPorMes = eventosPorMesRaw
                .Select(x => new
                {
                    Mes = $"{x.Year}-{x.Month:00}",
                    Total = x.Total
                })
                .OrderBy(x => x.Mes)
                .ToList();

            ViewBag.EventosPorMes = eventosPorMes;

            var topIdols = await _context.Idols
                .Include(i => i.Eventos)
                .Where(i => i.Activo)
                .OrderByDescending(i => i.Eventos.Count)
                .Take(5)
                .Select(i => new
                {
                    Nombre = i.NombreArtistico,
                    TotalEventos = i.Eventos.Count
                })
                .ToListAsync();

            ViewBag.TopIdols = topIdols;

            ViewBag.ReservasConfirmadas = await _context.Reservas
                .Where(r => r.Confirmada && r.FechaCancelacion == null)
                .CountAsync();
            ViewBag.ReservasPendientes = await _context.Reservas
                .Where(r => !r.Confirmada && r.FechaCancelacion == null)
                .CountAsync();
            ViewBag.ReservasCanceladas = await _context.Reservas
                .Where(r => r.FechaCancelacion != null)
                .CountAsync();

            return View();
        }
    }
}
