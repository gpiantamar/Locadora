using System.ComponentModel.DataAnnotations;

namespace CarLoc.Models;

public class Veiculo
{
    public int Id { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int? Ano { get; set; }
    public string? Placa { get; set; }
    public string? Cor { get; set; }
    public int? StatusId { get; set; }
    public int? CategoriaId { get; set; }

    //Relacionamento
    public virtual Status? Status {get; set;}
    public virtual Categoria? Categoria {get; set;}
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

}