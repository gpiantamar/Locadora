using System.ComponentModel.DataAnnotations;

namespace CarLoc.Models;

public class Cliente
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? RG { get; set; }
    public string? CPF { get; set; }
    [DataType(DataType.Date)]
    public DateTime? DataNascimento { get; set; }

    // Relacionamento
    public virtual List<Contrato>? Contratos {get; set;}
}