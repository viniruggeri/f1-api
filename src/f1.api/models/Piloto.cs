namespace F1.Api.Domain.Entities;

public class Piloto
{
    public int PilotoId { get; set; }
    public string Nome { get; private set; } = null!;
    public string Nacionalidade { get; private set; } = null!;
    public DateTime? DataNascimento { get; private set; }

    public int EquipeId { get; private set; }
    public Equipe Equipe { get; set; } = null!;

    public ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();

    protected Piloto() { }

    public Piloto(string nome, string nacionalidade, int equipeId, DateTime? dataNascimento = null)
    {
        ValidarNome(nome);
        ValidarNacionalidade(nacionalidade);
        if (dataNascimento.HasValue)
            ValidarDataNascimento(dataNascimento.Value);

        Nome = nome;
        Nacionalidade = nacionalidade;
        EquipeId = equipeId;
        DataNascimento = dataNascimento;
    }
    public void AtualizarNome(string novoNome)
    {
        ValidarNome(novoNome);
        Nome = novoNome;
    }

    public void AtualizarNacionalidade(string novaNacionalidade)
    {
        ValidarNacionalidade(novaNacionalidade);
        Nacionalidade = novaNacionalidade;
    }

    public void AtualizarEquipe(int novaEquipeId)
    {
        if (novaEquipeId <= 0)
            throw new ArgumentException("ID da equipe inválido.");
        EquipeId = novaEquipeId;
    }

    public void AtualizarDataNascimento(DateTime? novaData)
    {
        if (novaData.HasValue)
            ValidarDataNascimento(novaData.Value);
        DataNascimento = novaData;
    }

    public int CalcularIdade()
    {
        if (!DataNascimento.HasValue) return 0;
        
        var hoje = DateTime.Today;
        var idade = hoje.Year - DataNascimento.Value.Year;
        if (DataNascimento.Value.Date > hoje.AddYears(-idade)) idade--;
        return idade;
    }

    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do piloto não pode ser vazio.");
        if (nome.Length < 2 || nome.Length > 100)
            throw new ArgumentException("O nome do piloto deve ter entre 2 e 100 caracteres.");
    }

    private void ValidarNacionalidade(string nacionalidade)
    {
        if (string.IsNullOrWhiteSpace(nacionalidade))
            throw new ArgumentException("A nacionalidade não pode ser vazia.");
        if (nacionalidade.Length < 2 || nacionalidade.Length > 50)
            throw new ArgumentException("A nacionalidade deve ter entre 2 e 50 caracteres.");
    }

    private void ValidarDataNascimento(DateTime data)
    {
        if (data > DateTime.Today)
            throw new ArgumentException("A data de nascimento não pode ser futura.");
        if (data < new DateTime(1900, 1, 1))
            throw new ArgumentException("Data de nascimento inválida.");
        
        var idade = DateTime.Today.Year - data.Year;
        if (idade < 16)
            throw new ArgumentException("Piloto deve ter no mínimo 16 anos.");
    }
}
