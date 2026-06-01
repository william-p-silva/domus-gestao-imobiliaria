using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security; // Import necessário para o Hasher
using Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;
using Domus.Infrastructure.Data.Context;
using Domus.Infrastructure.Data.Repositories;
using Domus.Infrastructure.Data.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// 1. CONFIGURAÇÃO DE INFRAESTRUTURA DE DADOS (SQL Server 2025 Developer)
// ============================================================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Domus.Infrastructure")));

// ============================================================================
// 2. INJEÇÃO DE DEPENDÊNCIA (DI) - SERVIÇOS, REPOSITÓRIOS E SEGURANÇA
// ============================================================================

// Repositories & Transações (Compartilhando o escopo do DbContext)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IFuncaoRepository, FuncaoRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();

// Segurança (Correção: Injetando o serviço de Hash exigido pelo Use Case)
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Use Cases (Application)
builder.Services.AddScoped<CadastrarLocatarioUseCase>();

// ============================================================================
// 3. CONTROLLERS E DOCUMENTAÇÃO DA API
// ============================================================================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ============================================================================
// 4. PIPELINE DE REQUISIÇÕES HTTP (MIDDLEWARES)
// ============================================================================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ============================================================================
// 5. INICIALIZAÇÃO E AUTO-MIGRATION (Executado de forma isolada e segura)
// ============================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Aplica migrations pendentes e seeds nativos na inicialização do container/app
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro crítico ao criar ou aplicar as migrações automáticas no SQL Server.");
    }
}

app.Run();

// ============================================================================
// MEMO DE COMANDOS ÚTEIS PARA O TERMINAL:
// 
// Criar uma nova Migration:
// dotnet ef migrations add NomeDaMigration --project ..\Domus.Infrastructure --startup-project .
//
// Atualizar o banco manualmente (Caso não queira depender do auto-migrate):
// dotnet ef database update --project ..\Domus.Infrastructure --startup-project .
// ============================================================================