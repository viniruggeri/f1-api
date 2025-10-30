namespace F1.Api.Application.DTOs;

public class EquipeDTO
{
    public int EquipeId { get; set; }
    public string Nome { get; set; } = null!;
    public string Pais { get; set; } = null!;
    public int? AnoFundacao { get; set; }
}

public class CreateEquipeDTO
{
    public string Nome { get; set; } = null!;
    public string Pais { get; set; } = null!;
    public int? AnoFundacao { get; set; }
}

public class UpdateEquipeDTO
{
    public string Nome { get; set; } = null!;
    public string Pais { get; set; } = null!;
    public int? AnoFundacao { get; set; }
}

