using System.ComponentModel.DataAnnotations;

namespace CarLoc.Models;

public class Contrato
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int VeiculoId { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataInicio { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataTermino { get; set; }

    public double? ValorTotal { get; set; }

    // Relacionamentos
    public virtual Veiculo? Veiculo { get; set; }
    public virtual Cliente? Cliente { get; set; }

    // Método para calcular o valor total com base na diária
    public double CalcularValorTotal()
{
    if (Veiculo?.Categoria == null || Veiculo.Categoria.PrecoDiaria == null)
        return 0;

    int dias = (DataTermino - DataInicio).Days + 1;
    if (dias <= 0) dias = 1;

    return dias * Veiculo.Categoria.PrecoDiaria.Value;
}

}
