using System.ComponentModel.DataAnnotations;

namespace CarLoc.Models;

public class Status
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    
    public virtual List<Veiculo>? Veiculos {get; set;}
}