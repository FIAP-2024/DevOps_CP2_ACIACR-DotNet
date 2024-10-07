using CleanOceanic.Data;
using CleanOceanic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanOceanic.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Volunteer volunteer)
        {
            if (await _context.Volunteers.AnyAsync(c => c.Email == volunteer.Email))
            {
                TempData["msg"] = "E-mail já cadastrado!";
                return View(volunteer);
            }

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            TempData["msg"] = "Voluntário(a) cadastrado(a) com sucesso!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var volunteers = await _context.Volunteers.ToListAsync();
            return View(volunteers);
        }

        [HttpGet]
        public async Task<IActionResult> PesquisaNome(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }

            var volunteers = await _context.Volunteers
                .Where(c => c.Nome.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            if (!volunteers.Any())
            {
                TempData["msg"] = "Nenhum Voluntário(a) Encontrado(a)!!";
                return RedirectToAction("Index");
            }

            return View("Index", volunteers);
        }

        [HttpGet]
        public async Task<IActionResult> Visualizar(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                TempData["msg"] = "Voluntário(a) não encontrado(a)!";
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                TempData["msg"] = "Voluntário(a) não encontrado(a)!";
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Por favor, corrija os erros no formulário.";
                return View(volunteer);
            }

            var existingVolunteer = await _context.Volunteers.FindAsync(volunteer.IdUsuario);
            if (existingVolunteer == null)
            {
                TempData["msg"] = "Voluntário(a) não encontrado(a)!";
                return RedirectToAction("Index");
            }

            // Atualizar as propriedades conforme necessário
            existingVolunteer.Nome = volunteer.Nome;
            existingVolunteer.Genero = volunteer.Genero;
            existingVolunteer.Email = volunteer.Email;
            existingVolunteer.Senha = volunteer.Senha;
            existingVolunteer.Telefone = volunteer.Telefone;
            existingVolunteer.Address = volunteer.Address;

            await _context.SaveChangesAsync();

            TempData["msg"] = "Voluntário(a) atualizado(a) com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var volunteerToRemove = await _context.Volunteers.FindAsync(id);
            if (volunteerToRemove == null)
            {
                TempData["msg"] = "Voluntário(a) não encontrado(a)!";
                return RedirectToAction("Index");
            }

            _context.Volunteers.Remove(volunteerToRemove);
            await _context.SaveChangesAsync();

            TempData["msg"] = "Voluntário(a) removido(a) com sucesso!";
            return RedirectToAction("Index");
        }
    }
}