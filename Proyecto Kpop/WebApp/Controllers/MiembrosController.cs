using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;
using SistemaFanclub.Models;

namespace SistemaFanclub.Controllers
{
    public class MiembrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MiembrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, bool? soloActivos)
        {
            ViewData["SearchString"] = searchString;
            ViewData["SoloActivos"] = soloActivos ?? true;

            var miembros = _context.Miembros
                .Include(m => m.IdolFavorito)
                .Include(m => m.Usuario)
                .AsQueryable();

            if (soloActivos ?? true)
            {
                miembros = miembros.Where(m => m.Activo);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                miembros = miembros.Where(m =>
                    m.Nombre.Contains(searchString) ||
                    m.Apellido.Contains(searchString) ||
                    (m.Email != null && m.Email.Contains(searchString)) ||
                    (m.RolFanclub != null && m.RolFanclub.Contains(searchString))
                );
            }

            return View(await miembros.OrderBy(m => m.Apellido).ThenBy(m => m.Nombre).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros
                .Include(m => m.IdolFavorito)
                .Include(m => m.Usuario)
                .Include(m => m.Reservas)
                    .ThenInclude(r => r.Evento)
                .FirstOrDefaultAsync(m => m.MiembroId == id);

            if (miembro == null)
            {
                return NotFound();
            }

            return View(miembro);
        }

        public IActionResult Create()
        {
            ViewData["IdolFavoritoId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(u => u.Activo), "UsuarioId", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,Telefono,RolFanclub,IdolFavoritoId,UsuarioId,FechaIngreso,Biografia,Activo")] Miembro miembro)
        {
            if (ModelState.IsValid)
            {
                miembro.FechaRegistro = DateTime.Now;
                _context.Add(miembro);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Miembro registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdolFavoritoId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico", miembro.IdolFavoritoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(u => u.Activo), "UsuarioId", "NombreCompleto", miembro.UsuarioId);
            return View(miembro);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null)
            {
                return NotFound();
            }

            ViewData["IdolFavoritoId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico", miembro.IdolFavoritoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(u => u.Activo), "UsuarioId", "NombreCompleto", miembro.UsuarioId);
            return View(miembro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MiembroId,Nombre,Apellido,Email,Telefono,RolFanclub,IdolFavoritoId,UsuarioId,FechaIngreso,Biografia,Activo,FechaRegistro")] Miembro miembro)
        {
            if (id != miembro.MiembroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miembro);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Miembro actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembroExists(miembro.MiembroId))
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

            ViewData["IdolFavoritoId"] = new SelectList(_context.Idols.Where(i => i.Activo), "IdolId", "NombreArtistico", miembro.IdolFavoritoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(u => u.Activo), "UsuarioId", "NombreCompleto", miembro.UsuarioId);
            return View(miembro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros
                .Include(m => m.IdolFavorito)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MiembroId == id);

            if (miembro == null)
            {
                return NotFound();
            }

            return View(miembro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro != null)
            {
                _context.Miembros.Remove(miembro);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Miembro eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MiembroExists(int id)
        {
            return _context.Miembros.Any(e => e.MiembroId == id);
        }
    }
}
