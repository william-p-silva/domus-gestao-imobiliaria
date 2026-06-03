using Domus.Infrastructure.Data.Context;
using Domus.WebApi.Dependencies;
using Domus.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddProjectDependencies(); // Método de extensão para organizar a DI em um único local (DependencyInjectionConfig.cs)


// ============================================================================
// 3. TokenService - Implementação de geração de tokens JWT para autenticação e autorização
// ============================================================================
var key = Encoding.UTF8.GetBytes(
    builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
    options.TokenValidationParameters = new TokenValidationParameters
    {

    }
)

// ============================================================================
// 4. CONTROLLERS E DOCUMENTAÇÃO DA API
// ============================================================================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ============================================================================
// 5. Middleware global para tratamento de exceções personalizadas (ExceptionMiddleware.cs)
// ============================================================================
app.UseMiddleware<ExceptionMiddleware>();

// ============================================================================
// 6. PIPELINE DE REQUISIÇÕES HTTP (MIDDLEWARES)
// ============================================================================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ============================================================================
// 7. INICIALIZAÇÃO E AUTO-MIGRATION (Executado de forma isolada e segura)
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
// COMANDOS ÚTEIS PARA O TERMINAL:
// 
// Criar uma nova Migration:
// dotnet ef migrations add nomeMigration --project ..\Infrastructure 
//
// Atualizar o banco manualmente (Caso não queira depender do auto-migrate):
// dotnet ef database update --project ..\Infrastructure  .
// ============================================================================