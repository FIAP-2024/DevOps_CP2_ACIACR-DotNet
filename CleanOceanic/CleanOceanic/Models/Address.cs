using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CleanOceanic.Models;

[Table("Addresses")]
public class Address
{
    [HiddenInput]
    [Key]
    public int IdEndereco { get; set; }
    public required string Logradouro { get; set; }
    public required string Numero { get; set; }
    public required string Bairro { get; set; }
    public required string Cidade { get; set; }
    public required string SiglaEstado { get; set; }
    public required string Cep { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.Now;
}
