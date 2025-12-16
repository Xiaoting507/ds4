using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFanclub.Data;
using SistemaFanclub.Models;
using System.Security.Cryptography;
using System.Text;

namespace SistemaFanclub.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutenticacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UsuarioId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Email y contraseña son obligatorios";
                return View();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Activo);

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado o inactivo";
                return View();
            }

            if (usuario.PasswordHash != HashPassword(password))
            {
                TempData["ErrorMessage"] = "Contraseña incorrecta";
                return View();
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
            HttpContext.Session.SetString("UsuarioNombre", usuario.NombreCompleto);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

            usuario.UltimoAcceso = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"¡Bienvenido, {usuario.NombreCompleto}!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registro()
        {
            if (HttpContext.Session.GetInt32("UsuarioId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario usuario, string confirmarPassword)
        {
            if (string.IsNullOrEmpty(confirmarPassword) || usuario.PasswordHash != confirmarPassword)
            {
                ModelState.AddModelError("", "Las contraseñas no coinciden");
                return View(usuario);
            }

            var existeEmail = await _context.Usuarios
                .AnyAsync(u => u.Email == usuario.Email);

            if (existeEmail)
            {
                ModelState.AddModelError("Email", "Este email ya está registrado");
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                // Hashear contraseña
                usuario.PasswordHash = HashPassword(usuario.PasswordHash);
                usuario.Rol = "Miembro"; // Por defecto es Miembro
                usuario.Activo = true;
                usuario.FechaRegistro = DateTime.Now;

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registro exitoso. Ya puedes iniciar sesión";
                return RedirectToAction(nameof(Login));
            }

            return View(usuario);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Sesión cerrada exitosamente";
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
