namespace F1.Api.DTOs;

public class CorridaDTO
{
    public int CorridaId { get; set; }
    public string Nome { get; set; } = null!;
    public string Local { get; set; } = null!;
    public DateTime Data { get; set; }
}

