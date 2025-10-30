namespace F1.Api.Domain.Entities;

public class Equipe
{
    public int EquipeId { get; set; }
    public string Nome { get; private set; } = null!;
    public string Pais { get; private set; } = null!;
    public int? AnoFundacao { get; private set; }

    public ICollection<Piloto> Pilotos { get; set; } = new List<Piloto>();

    protected Equipe() { }

    public Equipe(string nome, string pais, int? anoFundacao = null)
    {
        ValidarNome(nome);
        ValidarPais(pais);
        if (anoFundacao.HasValue)
            ValidarAnoFundacao(anoFundacao.Value);

        Nome = nome;
        Pais = pais;
        AnoFundacao = anoFundacao;
    }

    public void AtualizarNome(string novoNome)
    {
        ValidarNome(novoNome);
        Nome = novoNome;
    }

    public void AtualizarPais(string novoPais)
    {
        ValidarPais(novoPais);
        Pais = novoPais;
    }

    public void AtualizarAnoFundacao(int? novoAno)
    {
        if (novoAno.HasValue)
            ValidarAnoFundacao(novoAno.Value);
        AnoFundacao = novoAno;
    }
    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da equipe não pode ser vazio.");
        if (nome.Length < 2 || nome.Length > 100)
            throw new ArgumentException("O nome da equipe deve ter entre 2 e 100 caracteres.");
    }

    private void ValidarPais(string pais)
    {
        if (string.IsNullOrWhiteSpace(pais))
            throw new ArgumentException("O país da equipe não pode ser vazio.");
        if (pais.Length < 2 || pais.Length > 50)
            throw new ArgumentException("O país da equipe deve ter entre 2 e 50 caracteres.");
    }

    private void ValidarAnoFundacao(int ano)
    {
        if (ano < 1900 || ano > DateTime.Now.Year)
            throw new ArgumentException("Ano de fundação inválido.");
    }
}
