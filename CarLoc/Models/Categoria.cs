using System.ComponentModel.DataAnnotations;

namespace CarLoc.Models;

public class Categoria
{
    public int Id {get; set;}
    public string? Nome {get; set;}
    public string? Descricao {get; set;}
    public double? PrecoDiaria {get; set;}
    


    // Relacionamento
    public virtual List<Veiculo>? Veiculos { get; set; }
}