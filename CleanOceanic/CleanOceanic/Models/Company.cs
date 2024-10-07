using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanOceanic.Models;

[Table("Companies")]
public class Company
{
    [HiddenInput]
    [Key]
    public long IdEmpresa { get; set; }
    public required string RazaoSocial { get; set; }
    public required string Cnpj { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public int QtdFuncionarios { get; set; }
    public required string Setor { get; set; }
    public required Address Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
