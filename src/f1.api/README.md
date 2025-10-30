# 🏎️ F1 API - CP2 Advanced Business Development with .NET

API RESTful para gerenciamento de dados da Fórmula 1, desenvolvida com .NET 8, aplicando **Clean Architecture** e **Domain-Driven Design (DDD)**.

---

## 👥 Integrantes do Grupo

- **RM560593** - Vinicius Lira Ruggeri
- **RM560431** - Barbara Bonome Filipus
- **RM560039** - Yasmin Pereira Silva

---

## 📋 Descrição do Projeto

Esta API foi desenvolvida como parte do CP2 da disciplina **Advanced Business Development with .NET** da FIAP. O projeto implementa um sistema completo de gerenciamento de dados da Fórmula 1, incluindo:

- **Equipes**: Gerenciamento de equipes da F1
- **Pilotos**: Controle de pilotos vinculados às equipes
- **Corridas**: Informações sobre as corridas do calendário
- **Resultados**: Resultados dos pilotos em cada corrida

### 🎯 Objetivos Alcançados

✅ CRUD completo para Equipes e Pilotos  
✅ Relacionamentos entre entidades (1:N e N:N)  
✅ Clean Architecture com separação de camadas  
✅ Domain-Driven Design com entidades ricas  
✅ Entity Framework Core com suporte a Oracle e MySQL  
✅ Migrations para versionamento do banco  
✅ DTOs e AutoMapper configurados  
✅ Documentação Swagger/OpenAPI  
✅ Validações de negócio nas entidades  
✅ Respostas HTTP apropriadas (200, 201, 204, 400, 404)  

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** e está organizado nas seguintes camadas:

```
f1.api/
├── Domain/                    # Camada de Domínio
│   ├── Entities/              # Entidades ricas com validações
│   └── Interfaces/            # Contratos dos repositórios
├── Application/               # Camada de Aplicação
│   ├── DTOs/                  # Data Transfer Objects
│   └── Mappings/              # AutoMapper profiles
├── Infrastructure/            # Camada de Infraestrutura
│   ├── Data/                  # DbContext e configurações EF
│   └── Repositories/          # Implementação dos repositórios
└── Presentation/              # Camada de Apresentação
    └── Controllers/           # Controllers da API
```

---

## 🚀 Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - Criação da API RESTful
- **Entity Framework Core 8.0** - ORM para acesso ao banco
- **Oracle.EntityFrameworkCore** - Provider para Oracle
- **Pomelo.EntityFrameworkCore.MySql** - Provider para MySQL
- **AutoMapper 12.0** - Mapeamento objeto-objeto
- **Swashbuckle.AspNetCore 6.5** - Documentação Swagger
- **InMemoryDatabase** - Para testes e desenvolvimento

---

## 📦 Instalação e Execução

### Pré-requisitos

- .NET 8.0 SDK instalado
- Oracle Database OU MySQL (ou use InMemory para testes)
- IDE (Rider, Visual Studio ou VS Code)

### Passo 1: Clonar o Repositório

```bash
git clone https://github.com/viniruggeri/f1-api.git
cd f1-api/src/f1.api
```

### Passo 2: Restaurar Pacotes

```bash
dotnet restore
```

### Passo 3: Configurar o Banco de Dados

Edite o arquivo `appsettings.json` com suas credenciais:

**Para Oracle:**
```json
"ConnectionStrings": {
  "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
}
```

**Para MySQL:**
```json
"ConnectionStrings": {
  "MySqlConnection": "Server=localhost;Database=f1database;User=root;Password=sua_senha;"
}
```

Depois, descomente a configuração desejada no `Program.cs`:

```csharp
// Para Oracle
builder.Services.AddDbContext<F1DbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// OU para MySQL
builder.Services.AddDbContext<F1DbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));
```

### Passo 4: Criar o Banco de Dados com Migrations

```bash
# Criar a primeira migration
dotnet ef migrations add InitialCreate

# Aplicar ao banco de dados
dotnet ef database update
```

### Passo 5: Executar a API

```bash
dotnet run
```

A API estará disponível em: `https://localhost:7000` ou `http://localhost:5000`

---

## 📚 Documentação da API (Rotas)

Acesse a documentação interativa Swagger em: **http://localhost:5000/**

### 🏁 Equipes

| Método | Rota | Descrição | Status Code |
|--------|------|-----------|-------------|
| GET | `/api/equipes` | Lista todas as equipes | 200 |
| GET | `/api/equipes/{id}` | Obtém uma equipe por ID | 200, 404 |
| POST | `/api/equipes` | Cria uma nova equipe | 201, 400 |
| PUT | `/api/equipes/{id}` | Atualiza uma equipe | 204, 400, 404 |
| DELETE | `/api/equipes/{id}` | Remove uma equipe | 204, 404 |

**Exemplo POST /api/equipes:**
```json
{
  "nome": "Red Bull Racing",
  "pais": "Áustria",
  "anoFundacao": 2005
}
```

### 🏎️ Pilotos

| Método | Rota | Descrição | Status Code |
|--------|------|-----------|-------------|
| GET | `/api/pilotos` | Lista todos os pilotos | 200 |
| GET | `/api/pilotos/{id}` | Obtém um piloto por ID | 200, 404 |
| GET | `/api/pilotos/equipe/{equipeId}` | Lista pilotos de uma equipe | 200, 404 |
| POST | `/api/pilotos` | Cria um novo piloto | 201, 400 |
| PUT | `/api/pilotos/{id}` | Atualiza um piloto | 204, 400, 404 |
| DELETE | `/api/pilotos/{id}` | Remove um piloto | 204, 404 |

**Exemplo POST /api/pilotos:**
```json
{
  "nome": "Max Verstappen",
  "nacionalidade": "Holandês",
  "dataNascimento": "1997-09-30",
  "equipeId": 1
}
```

---

## 🧪 Testando a API

### Usando cURL

```bash
# Listar todas as equipes
curl -X GET http://localhost:5000/api/equipes

# Criar uma nova equipe
curl -X POST http://localhost:5000/api/equipes \
  -H "Content-Type: application/json" \
  -d '{"nome":"Ferrari","pais":"Itália","anoFundacao":1950}'

# Criar um piloto
curl -X POST http://localhost:5000/api/pilotos \
  -H "Content-Type: application/json" \
  -d '{"nome":"Lewis Hamilton","nacionalidade":"Britânico","dataNascimento":"1985-01-07","equipeId":2}'
```

### Usando o Swagger

1. Acesse `http://localhost:5000/`
2. Expanda os endpoints desejados
3. Clique em "Try it out"
4. Preencha os dados e execute

---

## 🗃️ Estrutura do Banco de Dados

### Entidades e Relacionamentos

- **Equipe** (1) → (N) **Piloto**
- **Piloto** (N) → (N) **Corrida** (através de **Resultado**)

### Diagrama ER Simplificado

```
┌─────────────┐       ┌─────────────┐       ┌─────────────┐
│   Equipe    │1    N │   Piloto    │N    N │   Corrida   │
├─────────────┤───────├─────────────┤───────├─────────────┤
│ EquipeId PK │       │ PilotoId PK │       │ CorridaId PK│
│ Nome        │       │ Nome        │       │ Nome        │
│ Pais        │       │ Nacionalidade│      │ Local       │
│ AnoFundacao │       │ DataNasc    │       │ Data        │
└─────────────┘       │ EquipeId FK │       └─────────────┘
                      └─────────────┘
                             │
                             │ N:N
                             ▼
                      ┌─────────────┐
                      │  Resultado  │
                      ├─────────────┤
                      │ResultadoId PK│
                      │ PilotoId FK  │
                      │ CorridaId FK │
                      │ Posicao      │
                      │ Pontos       │
                      └─────────────┘
```

---

## ✨ Funcionalidades Especiais

### Validações de Negócio

- Nome da equipe: 2-100 caracteres
- País: 2-50 caracteres
- Ano de fundação: entre 1900 e ano atual
- Piloto deve ter no mínimo 16 anos
- Data de nascimento não pode ser futura

### Entidades Ricas (DDD)

As entidades possuem comportamento próprio e encapsulam suas regras de negócio:

```csharp
var equipe = new Equipe("Red Bull Racing", "Áustria", 2005);
equipe.AtualizarNome("Red Bull Racing Honda"); // Validação automática
```

### DTOs e AutoMapper

Separação clara entre entidades de domínio e objetos de transferência:
- `CreateEquipeDTO` - Para criação
- `UpdateEquipeDTO` - Para atualização
- `EquipeDTO` - Para leitura

---

## 📝 Migrations

### Comandos Úteis

```bash
# Adicionar uma nova migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations pendentes
dotnet ef database update

# Reverter para uma migration específica
dotnet ef database update NomeDaMigration

# Remover a última migration
dotnet ef migrations remove

# Listar migrations
dotnet ef migrations list
```

---

## 🔒 Segurança

⚠️ **IMPORTANTE**: Nunca commite senhas ou credenciais no GitHub!

### Boas Práticas Implementadas:

- ConnectionStrings no `appsettings.json` (adicione ao `.gitignore`)
- Use `dotnet user-secrets` para desenvolvimento local
- Configure variáveis de ambiente na produção

```bash
# Exemplo usando user-secrets
dotnet user-secrets set "ConnectionStrings:OracleConnection" "sua-connection-string"
```

---

## 🐛 Tratamento de Erros

A API retorna respostas padronizadas:

```json
// Erro 400 - Bad Request
{
  "message": "O nome da equipe deve ter entre 2 e 100 caracteres."
}

// Erro 404 - Not Found
{
  "message": "Piloto com ID 999 não encontrado."
}
```

---

## 🚀 Melhorias Futuras

- [ ] Autenticação e Autorização (JWT)
- [ ] Paginação nas listagens
- [ ] Filtros e ordenação
- [ ] Cache com Redis
- [ ] Testes unitários e de integração
- [ ] CI/CD com GitHub Actions
- [ ] Docker e Docker Compose
- [ ] Health checks
- [ ] Versionamento da API

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais como parte do CP2 da FIAP.

---

## 🙏 Agradecimentos

- FIAP - Faculdade de Informática e Administração Paulista
- Prof. [Nome do Professor]
- Turma 2TDSPN

---

## 📞 Contato

Para dúvidas ou sugestões, entre em contato com os integrantes do grupo.

---

**"Faça o teu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda."** — Mario Sergio Cortella

