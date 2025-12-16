using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;

namespace SistemaFanclub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId.HasValue)
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
                if (usuario != null)
                {
                    ViewBag.UsuarioNombre = usuario.NombreCompleto;
                    ViewBag.UsuarioRol = usuario.Rol;
                }
            }

            ViewBag.TotalIdols = await _context.Idols.Where(i => i.Activo).CountAsync();
            ViewBag.TotalMiembros = await _context.Miembros.Where(m => m.Activo).CountAsync();
            ViewBag.TotalEventos = await _context.Eventos
                .Where(e => e.Estado != "Cancelado")
                .CountAsync();
            ViewBag.ProximosEventos = await _context.Eventos
                .Where(e => e.FechaEvento >= DateTime.Today && e.Estado == "Programado")
                .CountAsync();

            var proximosEventos = await _context.Eventos
                .Include(e => e.Idol)
                .Where(e => e.FechaEvento >= DateTime.Today && e.Estado == "Programado")
                .OrderBy(e => e.FechaEvento)
                .ThenBy(e => e.HoraInicio)
                .Take(5)
                .ToListAsync();

            return View(proximosEventos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
