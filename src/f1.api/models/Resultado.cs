namespace F1.Api.Domain.Entities;

public class Resultado
{
    public int ResultadoId { get; set; }
    public int PilotoId { get; set; }
    public Piloto Piloto { get; set; } = null!;

    public int CorridaId { get; set; }
    public Corrida Corrida { get; set; } = null!;

    public int Posicao { get; set; }
    public int Pontos { get; set; }
}
