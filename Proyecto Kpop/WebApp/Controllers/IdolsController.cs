using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;
using SistemaFanclub.Models;

namespace SistemaFanclub.Controllers
{
    public class IdolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string tipoIdol)
        {
            ViewData["SearchString"] = searchString;
            ViewData["TipoIdol"] = tipoIdol;

            var idols = _context.Idols
                .Include(i => i.Agencia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                idols = idols.Where(i =>
                    i.NombreArtistico.Contains(searchString) ||
                    (i.NombreReal != null && i.NombreReal.Contains(searchString)) ||
                    (i.Fandom != null && i.Fandom.Contains(searchString))
                );
            }

            if (!string.IsNullOrEmpty(tipoIdol))
            {
                idols = idols.Where(i => i.TipoIdol == tipoIdol);
            }

            return View(await idols.OrderBy(i => i.NombreArtistico).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idol = await _context.Idols
                .Include(i => i.Agencia)
                .Include(i => i.Eventos)
                .Include(i => i.MiembrosFans)
                .FirstOrDefaultAsync(m => m.IdolId == id);

            if (idol == null)
            {
                return NotFound();
            }

            return View(idol);
        }

        public IActionResult Create()
        {
            ViewData["AgenciaId"] = new SelectList(_context.Agencias.Where(a => a.Activa), "AgenciaId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreArtistico,NombreReal,TipoIdol,AgenciaId,FechaDebut,Genero,Fandom,Pais,Biografia,FotoURL,Activo")] Idol idol)
        {
            if (ModelState.IsValid)
            {
                idol.FechaRegistro = DateTime.Now;
                _context.Add(idol);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Idol creado exitosamente";
                return RedirectToAction(nameof(Index));
            }

            ViewData["AgenciaId"] = new SelectList(_context.Agencias.Where(a => a.Activa), "AgenciaId", "Nombre", idol.AgenciaId);
            return View(idol);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idol = await _context.Idols.FindAsync(id);
            if (idol == null)
            {
                return NotFound();
            }

            ViewData["AgenciaId"] = new SelectList(_context.Agencias.Where(a => a.Activa), "AgenciaId", "Nombre", idol.AgenciaId);
            return View(idol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdolId,NombreArtistico,NombreReal,TipoIdol,AgenciaId,FechaDebut,Genero,Fandom,Pais,Biografia,FotoURL,Activo,FechaRegistro")] Idol idol)
        {
            if (id != idol.IdolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idol);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Idol actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdolExists(idol.IdolId))
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

            ViewData["AgenciaId"] = new SelectList(_context.Agencias.Where(a => a.Activa), "AgenciaId", "Nombre", idol.AgenciaId);
            return View(idol);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idol = await _context.Idols
                .Include(i => i.Agencia)
                .FirstOrDefaultAsync(m => m.IdolId == id);

            if (idol == null)
            {
                return NotFound();
            }

            return View(idol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idol = await _context.Idols.FindAsync(id);
            if (idol != null)
            {
                _context.Idols.Remove(idol);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Idol eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IdolExists(int id)
        {
            return _context.Idols.Any(e => e.IdolId == id);
        }
    }
}
