using CleanOceanic.Data;
using CleanOceanic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanOceanic.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para exibir a página de cadastro de empresa
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        // Método para cadastrar uma nova empresa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Company company)
        {
            // Verifica se o CNPJ já está cadastrado
            if (await _context.Companies.AnyAsync(c => c.Cnpj == company.Cnpj))
            {
                TempData["msg"] = "CNPJ já cadastrado!";
                return View(company);
            }

            // Adiciona a nova empresa ao DbSet
            _context.Companies.Add(company);
            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados

            TempData["msg"] = "Empresa cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        // Método para exibir a lista de empresas
        public async Task<IActionResult> Index()
        {
            var companies = await _context.Companies.ToListAsync();
            return View(companies);
        }

        // Método para pesquisar empresas por nome
        [HttpGet]
        public async Task<IActionResult> PesquisaNome(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }

            var companies = await _context.Companies
                .Where(c => c.RazaoSocial.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            if (!companies.Any())
            {
                TempData["msg"] = "Nenhuma empresa encontrada!";
                return RedirectToAction("Index");
            }

            return View("Index", companies);
        }

        // Método para visualizar detalhes de uma empresa
        [HttpGet]
        public async Task<IActionResult> Visualizar(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                TempData["msg"] = "Empresa não encontrada!";
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // Método para editar detalhes de uma empresa
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                TempData["msg"] = "Empresa não encontrada!";
                return RedirectToAction("Index");
            }

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Company company)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Por favor, corrija os erros no formulário.";
                return View(company);
            }

            var existingCompany = await _context.Companies.FindAsync(company.IdEmpresa);
            if (existingCompany == null)
            {
                TempData["msg"] = "Empresa não encontrada!";
                return RedirectToAction("Index");
            }

            // Atualiza as propriedades da empresa conforme necessário
            existingCompany.RazaoSocial = company.RazaoSocial;
            existingCompany.Telefone = company.Telefone;
            existingCompany.Email = company.Email;
            existingCompany.QtdFuncionarios = company.QtdFuncionarios;
            existingCompany.Address = company.Address;

            await _context.SaveChangesAsync();

            TempData["msg"] = "Empresa atualizada com sucesso!";
            return RedirectToAction("Index");
        }

        // Método para remover uma empresa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var companyToRemove = await _context.Companies.FindAsync(id);
            if (companyToRemove == null)
            {
                TempData["msg"] = "Empresa não encontrada!";
                return RedirectToAction("Index");
            }

            _context.Companies.Remove(companyToRemove);
            await _context.SaveChangesAsync();

            TempData["msg"] = "Empresa removida com sucesso!";
            return RedirectToAction("Index");
        }
    }
}