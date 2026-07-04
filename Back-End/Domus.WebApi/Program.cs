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
builder.Services.AddProjectDependencies(builder.Configuration); // Método de extensão para organizar a DI em um único local (DependencyInjectionConfig.cs)


// ============================================================================
// 3. TokenService - Implementação de geração de tokens JWT para autenticação e autorização
// ============================================================================
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("A chave secreta do JWT (Jwt:Key) não foi configurada no appsettings.json.");

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// ============================================================================
// 4. CONTROLLERS E DOCUMENTAÇÃO DA API
// ============================================================================
builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        var requirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        };

        document.SecurityRequirements = new List<Microsoft.OpenApi.Models.OpenApiSecurityRequirement> { requirement };

        var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Insira o token JWT gerado no login neste formato: {seu_token}"
        };

        document.Components ??= new Microsoft.OpenApi.Models.OpenApiComponents();
        document.Components.SecuritySchemes.Add("Bearer", scheme);

        return Task.CompletedTask;
    });
});

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

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Domus API v1");
        options.RoutePrefix = "swagger"; // Define a URL de acesso
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
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


// Documentação:
// http://localhost:5038/swagger/index.html
// ===============================================================================================
// COMANDOS ÚTEIS PARA O TERMINAL:
// 
// Criar uma nova Migration:
// dotnet ef migrations add ControleExclusao --project ..\Infrastructure 
//
// Atualizar o banco manualmente (Caso não queira depender do auto-migrate):
// dotnet ef database update --project ..\Infrastructure  
//
//
// Salvar Chaves de configuração sensíveis (ex: JWT) usando Secret Manager (Desenvolvimento local):
// dotnet user-secrets list // Listar chaves e valores atuais
// dotnet dotnet user-secrets set chave valor // Adicionar ou atualizar uma chave-valor
// dotnet user-secrets remove chave // Remover uma chave específica
// =================================================================================================