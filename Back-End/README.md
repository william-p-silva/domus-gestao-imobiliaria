# ⚙️ DOMUS — Back-End (`Domus.WebApi` e camadas)

Este documento detalha a arquitetura e as tecnologias do back-end do DOMUS: uma API REST em **.NET 9**, estruturada em **Clean Architecture**, responsável por autenticação, gestão de imóveis, contratos, parcelas, avaliações, reclamações e chat em tempo real.

---

## 🧰 Stack tecnológica

| Categoria | Tecnologia | Uso no projeto |
|---|---|---|
| Runtime/Framework | **.NET 9** / ASP.NET Core Web API | Base da aplicação |
| ORM | **Entity Framework Core 9** | Mapeamento objeto-relacional e migrations |
| Banco de dados | **SQL Server** | Persistência relacional |
| Autenticação | **JWT Bearer** (via cookie `HttpOnly`) | Login e autorização |
| Hash de senha | **BCrypt.Net-Next** | Armazenamento seguro de credenciais |
| E-mail | **MailKit** | Confirmação de conta, recuperação de senha |
| Tempo real | **ASP.NET Core SignalR** | Hub de chat por imóvel |
| Documentação de API | **Swagger UI** + **Scalar** | Exploração e teste dos endpoints |
| Testes | **xUnit** (Domus.UnitTests) | Testes unitários dos casos de uso |

---

## 🏗️ Clean Architecture — visão geral

O back-end é dividido em 5 projetos (.csproj) com **regra de dependência de dentro para fora**: camadas internas nunca conhecem as externas.

```
                 ┌─────────────────────┐
                 │     Domus.WebApi     │  ← Controllers, Hubs, Middlewares, DI
                 └──────────┬──────────┘
                            │ depende de
                 ┌──────────▼──────────┐
                 │ Domus.Infrastructure │  ← EF Core, Repositórios, Segurança, E-mail
                 └──────────┬──────────┘
                            │ depende de
                 ┌──────────▼──────────┐
                 │  Domus.Application   │  ← Casos de uso, DTOs, Interfaces
                 └──────────┬──────────┘
                            │ depende de
                 ┌──────────▼──────────┐
                 │    Domus.Domain      │  ← Entidades, Value Objects, Exceptions
                 └─────────────────────┘

                 Domus.UnitTests → testa Domus.Application (casos de uso)
```

A `Domain` não referencia nenhum outro projeto — é o núcleo puro de regras de negócio. A `Application` conhece apenas o `Domain` e define **interfaces** (contratos) que a `Infrastructure` implementa. Isso permite trocar o banco de dados, o provedor de e-mail ou o mecanismo de hashing sem alterar a lógica de negócio.

---

## 🧬 `Domus.Domain` — o núcleo do negócio

Entidades ricas (não são apenas "sacos de propriedades"): setters privados, construtores que validam suas próprias regras e lançam exceções específicas quando os dados são inválidos.

```
Domus.Domain/
├── Entity/          # Usuario, Imovel, Contrato, ParcelaAluguel, Chat, Avaliacao, Reclamacao...
├── ValueObjects/     # CPF, Email, Nome, Celular, NomeChat
├── Enums/           # StatusContrato, StatusImovel, StatusPagamento, Perfil, EstadoChat...
└── Exceptions/       # DomainException (abstrata) → BusinessRuleException, NotFoundException, ValidationException
```

**Exemplo real** — a entidade `Chat` só pode ser criada a partir de um `Imovel` válido:
```csharp
public Chat(Imovel imovel)
{
    if (imovel is null)
        throw new NotFoundException("Imóvel não encontrado.");
    ...
    Chat_ID = Guid.NewGuid();
    Estado = EstadoChat.Ativo;
}
```

**Value Objects** encapsulam validação de formato — por exemplo, `CPF.Create("111.222.333-44")` remove a máscara e valida os dígitos verificadores antes de permitir a criação do objeto; um CPF inválido nunca chega a existir como instância válida no sistema.

---

## 🧩 `Domus.Application` — casos de uso (Use Cases)

Em vez de "Services" genéricos com muitos métodos, o projeto usa **um caso de uso por ação de negócio** — uma classe, um método `Execute`, uma responsabilidade:

```
Domus.Application/
├── UseCases/
│   ├── UsuarioUseCase/
│   │   ├── AuthUseCase/LoginUseCase.cs
│   │   ├── LocadorUseCase/CadastrarLocadorUseCase.cs
│   │   ├── LocatarioUseCase/CadastrarLocatarioUseCase.cs
│   │   └── AdminUseCase/CadastrarAdminUseCase.cs
│   ├── ImovelUseCase/           (Cadastrar, Excluir, Atualizar, CicloDeVida/Aprovar, Listar/*)
│   ├── ContratoUseCase/         (Cadastrar, CicloDeVida/Assinar, DisponibilizarParaAssinatura, Rejeitar)
│   ├── ChatUseCase/             (CadastrarChatImovel, EnviarMensagem, Listar/*)
│   └── AvaliacaoUseCases/CriarAvaliacaoUseCase.cs
├── DTOs/           # Request/Response por funcionalidade
├── Mappers/        # Conversão Entidade ↔ DTO
└── Interfaces/
    ├── Repositories/   # IUsuarioRepository, IImovelRepository, IUnitOfWork...
    ├── Security/       # IPasswordHasher, ITokenService
    ├── Email/          # IEmailService
    └── Notifications/  # IChatHubNotifier
```

**Exemplo real** — `LoginUseCase` depende apenas de *interfaces*, nunca de implementações concretas:
```csharp
public class LoginUseCase(
    IUsuarioRepository usuarioRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
{
    public async Task<LoginResponse> Execute(LoginRequest request, CancellationToken ct)
    {
        var usuario = await usuarioRepository.BuscarPorEmailAsync(request.Email, ct);
        // valida senha com o hasher, gera token, retorna DTO
    }
}
```
Isso torna cada caso de uso **fácil de testar isoladamente** (com mocks/fakes das interfaces) — é exatamente o que o `Domus.UnitTests` faz.

---

## 🗄️ `Domus.Infrastructure` — implementações concretas

```
Domus.Infrastructure/
├── Data/
│   ├── Context/AppDbContext.cs        # DbContext com todos os DbSets
│   ├── Configurations/                # Fluent API — uma classe por entidade (IEntityTypeConfiguration)
│   ├── Repositories/                  # Implementações concretas + UnitOfWork
│   ├── Security/                      # PasswordHasher (BCrypt) e TokenService (JWT)
│   ├── Email/                         # EmailService (MailKit) + EmailSettings
│   └── Seed/                          # Dados iniciais (ex.: papéis/funções padrão)
└── Migrations/                        # Histórico de migrations do EF Core (15 até o momento)
```

- **Mapeamento por convenção + Fluent API**: cada entidade tem sua própria classe de configuração (`ImovelConfiguration`, `ContratoConfiguration`, etc.), aplicadas automaticamente via `modelBuilder.ApplyConfigurationsFromAssembly(...)` no `AppDbContext`.
- **Repository + Unit of Work**: cada repositório cuida de consultas/gravações de uma entidade; o `UnitOfWork.CommitAsync()` centraliza o `SaveChangesAsync()`, garantindo que várias operações de um caso de uso sejam persistidas em uma única transação.
- **Segurança**: `PasswordHasher` usa `BCrypt.Net.BCrypt.HashPassword`/`Verify`; `TokenService` monta o JWT com claims de `NameIdentifier`, `Name`, `Email` e uma claim de `Role` por perfil do usuário (Locador, Locatário, Administrador), com expiração de 3 horas.

---

## 🌐 `Domus.WebApi` — API REST

```
Domus.WebApi/
├── Controllers/     # Um subdiretório por recurso; controllers separados por verbo HTTP
│   ├── UsuarioControllers/ (AuthController, AdminController, ConfirmarController...)
│   ├── ImovelController/   (ImovelGetController, ImovelPostController, ImovelPutController, ImovelDeleteController)
│   ├── ContratoController/
│   ├── ChatController/
│   └── AvaliacaoController/
├── Hubs/ChatImovelHub.cs           # Hub SignalR (JoinChatGroup / LeaveChatGroup)
├── Services/Chat/ChatHubNotifier.cs # Implementa IChatHubNotifier, notifica clientes conectados
├── Middlewares/
│   ├── DomusExceptionHandler.cs     # IExceptionHandler global (padrão .NET 8+)
│   └── ExceptionMiddleware.cs
├── Dependencies/DependencyInjectionConfig.cs  # Todo o registro de DI centralizado aqui
└── Program.cs
```

### Autenticação
- Login gera um JWT e o devolve dentro de um **cookie `HttpOnly`, `SameSite=Strict`** (`auth_token`) — o front-end nunca manipula o token diretamente.
- O middleware de autenticação JWT foi configurado para ler o token do cookie (`OnMessageReceived` → `context.Request.Cookies["auth_token"]`), em vez do header `Authorization` padrão.
- Endpoint `POST /domus/auth/me` retorna os dados do usuário logado a partir das *claims* do token — é o que o front-end usa para restaurar a sessão ao recarregar a página.

### Tratamento de erros centralizado
Um único `IExceptionHandler` (`DomusExceptionHandler`) converte exceções de domínio em respostas HTTP padronizadas (`application/problem+json`):

| Exceção | Status HTTP |
|---|---|
| `BusinessRuleException` | 400 |
| `ValidationException` | 409 |
| `NotFoundException` | 404 |
| `DomainException` (genérica) | 400 |
| `UnauthorizedAccessException` | 401 |
| Qualquer outra | 500 |

Isso evita `try/catch` repetido em cada controller — os casos de uso apenas lançam a exceção de domínio apropriada.

### Injeção de dependência
Toda a DI do projeto (repositórios, casos de uso, segurança, e-mail, SignalR) fica centralizada em **um único método de extensão**, `AddProjectDependencies`, chamado a partir do `Program.cs` — facilita localizar e adicionar novas dependências sem poluir o `Program.cs`.

### Documentação da API
- **Swagger UI** em `/swagger` e **Scalar** (interface alternativa) em desenvolvimento, ambos gerados a partir do OpenAPI nativo do .NET.
- Suporte a autenticação Bearer documentado no schema OpenAPI.

### CORS
Política nomeada `VueAppPolicy`, liberando `http://localhost:5173` e `http://localhost:3000` (portas padrão do Vite) com `AllowCredentials()` — necessário para o cookie de autenticação funcionar entre front e back em domínios/portas diferentes.

### Migrations automáticas
No startup, a aplicação roda `context.Database.Migrate()` dentro de um escopo isolado, aplicando migrations pendentes automaticamente (com log de erro caso falhe) — útil para ambientes de desenvolvimento e containers.

---

## 💳 Sobre a integração Pix

O modelo de dados já contempla os campos de Pix (`PixCopiaCola` na entidade `ParcelaAluguel`), mas a geração do payload **ainda é simulada** — ao criar as parcelas de um contrato, o código gera uma string de exemplo (`"PIX_FAKE_CHAVE_CONTRATO_..."`) no lugar de uma chamada real a um provedor de pagamento. Ou seja: a estrutura para a integração está pronta, mas o gateway de pagamento real ainda não foi plugado.

---

## 🧪 Testes (`Domus.UnitTests`)

- Testes unitários dos **casos de uso** da camada `Application`, usando **Fixtures** (`UsuarioFixture`, `ContratoFixture`) para montar cenários de teste reutilizáveis.
- Cobrem fluxos como cadastro de locatário, ciclo de vida de contrato (disponibilizar para assinatura, assinar, rejeitar), aprovação de imóvel e criação de chat.
- Por dependerem apenas de interfaces (repositórios, segurança), os casos de uso são testados com dublês (fakes/mocks), sem precisar de banco de dados real.

---

## 🚀 Como rodar localmente

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- SQL Server (local, Docker ou Azure SQL)

### Configuração
Defina as chaves sensíveis via `appsettings.Development.json` ou, preferencialmente, via **User Secrets**:
```bash
cd Domus.WebApi
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Database=Domus;..."
dotnet user-secrets set "Jwt:Key" "uma-chave-secreta-bem-grande"
dotnet user-secrets set "Jwt:Issuer" "Domus"
dotnet user-secrets set "Jwt:Audience" "DomusApp"
dotnet user-secrets set "EmailSettings:..." "..."
```

### Executar
```bash
cd Back-End
dotnet restore
dotnet run --project Domus.WebApi
```
As migrations são aplicadas automaticamente no startup. A API sobe (por padrão) em `http://localhost:5038`, com documentação em `http://localhost:5038/swagger`.

### Rodar os testes
```bash
dotnet test Domus.UnitTests
```

### Comandos úteis de migration
```bash
# Criar uma nova migration
dotnet ef migrations add NomeDaMigration --project Domus.Infrastructure --startup-project Domus.WebApi

# Aplicar manualmente (sem depender do auto-migrate)
dotnet ef database update --project Domus.Infrastructure --startup-project Domus.WebApi
```

---

## 📌 Observações e pontos de atenção

- A integração Pix é **mockada** (`PIX_FAKE_...`) — não há gateway de pagamento real conectado ainda.
- O cookie de autenticação está com `Secure = false` no `AuthController` — em produção (HTTPS), isso deve virar `true` para evitar o envio do cookie em conexões não criptografadas.
- Os controllers de imóvel são separados por verbo HTTP (`ImovelGetController`, `ImovelPostController`, etc.) em vez de um único `ImovelController` REST completo — uma escolha de organização válida, mas que foge um pouco da convenção mais comum de "um controller por recurso".