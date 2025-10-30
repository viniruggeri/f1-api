namespace F1.Api.Application.DTOs;

public class PilotoDTO
{
    public int PilotoId { get; set; }
    public string Nome { get; set; } = null!;
    public string Nacionalidade { get; set; } = null!;
    public DateTime? DataNascimento { get; set; }
    public int EquipeId { get; set; }
    public string? EquipeNome { get; set; }
    public int? Idade { get; set; }
}

public class CreatePilotoDTO
{
    public string Nome { get; set; } = null!;
    public string Nacionalidade { get; set; } = null!;
    public DateTime? DataNascimento { get; set; }
    public int EquipeId { get; set; }
}

public class UpdatePilotoDTO
{
    public string Nome { get; set; } = null!;
    public string Nacionalidade { get; set; } = null!;
    public DateTime? DataNascimento { get; set; }
    public int EquipeId { get; set; }
}

