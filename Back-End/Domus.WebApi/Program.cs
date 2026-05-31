using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados SQL Server 2025 Developer
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, x => 
    x.MigrationsAssembly("Domus.Infrastructure")));

builder.Services.AddControllers();

builder.Services.AddOpenApi();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // O método .Migrate() analisa se o banco de dados existe. 
        // Se não existir, ele CRIA o banco de dados no SQL Server e executa 
        // todas as migrations estruturais e os registros de semente (Seed).
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao criar ou aplicar as migrações no SQL Server.");
    }
}

app.Run();

//Comando para as migrations
// dotnet ef migrations add AdicionandoFuncoesSeed --project ..\Infrastructure

//Comando para rodar o Update do Banco
// dotnet ef database update --project ..\Infrastructure