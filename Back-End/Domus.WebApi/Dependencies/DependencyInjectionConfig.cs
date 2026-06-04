using Microsoft.Extensions.DependencyInjection;

using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;
using Domus.Application.UseCases.AvaliacaoUseCases;
using Domus.Application.UseCases.ImovelUseCase;
using Domus.Application.UseCases.UsuarioUseCase.AdminUseCase;
using Domus.Application.UseCases.UsuarioUseCase.LocadorUseCase;
using Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;
using Domus.Infrastructure.Data.Repositories;
using Domus.Infrastructure.Data.Security;
using Domus.Application.UseCases.ContratoUseCase;
using Domus.Application.UseCases.UsuarioUseCase.AuthUseCase;

namespace Domus.WebApi.Dependencies;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
    {
        // Repositories & Transações
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IFuncaoRepository, FuncaoRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
        services.AddScoped<IImovelRepository, ImovelRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<IContratoRepository, ContratoRepository>();


        // Segurança
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        // Use Cases
        services.AddScoped<CadastrarLocatarioUseCase>();
        services.AddScoped<CadastrarLocadorUseCase>();
        services.AddScoped<CadastrarAdminUseCase>();
        services.AddScoped<CadastrarImovelUseCase>();
        services.AddScoped<CriarAvaliacaoUseCase>();
        services.AddScoped<CadastrarContratoUseCase>();
        services.AddScoped<LoginUseCase>();

        return services;
    }
}
