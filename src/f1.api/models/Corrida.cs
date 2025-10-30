namespace F1.Api.Domain.Entities;

public class Corrida
{
    public int CorridaId { get; set; } 
    public string Nome { get; set; } = null!;
    public string Local { get; set; } = null!;
    public DateTime Data { get; set; }

    public ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
