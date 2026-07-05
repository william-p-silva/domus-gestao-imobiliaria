using Domus.Application.Interfaces.Email;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;
using Domus.Application.UseCases.AvaliacaoUseCases;
using Domus.Application.UseCases.ContratoUseCase;
using Domus.Application.UseCases.ContratoUseCase.CicloDeVida;
using Domus.Application.UseCases.ContratoUseCase.Listar;
using Domus.Application.UseCases.ImovelUseCase;
using Domus.Application.UseCases.ImovelUseCase.Atualizar;
using Domus.Application.UseCases.ImovelUseCase.CicloDeVida;
using Domus.Application.UseCases.ImovelUseCase.Listar;
using Domus.Application.UseCases.UsuarioUseCase;
using Domus.Application.UseCases.UsuarioUseCase.AdminUseCase;
using Domus.Application.UseCases.UsuarioUseCase.Atualizar;
using Domus.Application.UseCases.UsuarioUseCase.AuthUseCase;
using Domus.Application.UseCases.UsuarioUseCase.Listar;
using Domus.Application.UseCases.UsuarioUseCase.LocadorUseCase;
using Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;
using Domus.Infrastructure.Data.Email;
using Domus.Infrastructure.Data.Repositories;
using Domus.Infrastructure.Data.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Domus.WebApi.Dependencies;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //Interfaces & Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IFuncaoRepository, FuncaoRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
        services.AddScoped<IImovelRepository, ImovelRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<IContratoRepository, ContratoRepository>();


        //Segurança
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        //Email
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();

        //Use Cases
        //Usuário
        services.AddScoped<ConfirmarEmailUseCase>(); // Confirmar email
        services.AddScoped<AdicionarInfosExtrasUseCase>();
        services.AddScoped<BuscarPerfilUseCase>();
        services.AddScoped<ExcluirContaUseCase>();
        services.AddScoped<AlterarInfosUseCase>();
        services.AddScoped<AlterarInfosImovelUseCase>();
        //Locatario
        services.AddScoped<CadastrarLocatarioUseCase>();
        //Locador
        services.AddScoped<CadastrarLocadorUseCase>();
        //Admin
        services.AddScoped<CadastrarAdminUseCase>();
        //Imovel
        services.AddScoped<CadastrarImovelUseCase>();
        services.AddScoped<AprovarImovelUseCase>();
        services.AddScoped<ListarImoveisAprovadosUseCase>();
        services.AddScoped<ExcluirImovelUseCase>();
        services.AddScoped<AlterarInfosImovelUseCase>();
        //Avaliacao
        services.AddScoped<CriarAvaliacaoUseCase>();
        //Contrato
        services.AddScoped<CadastrarContratoUseCase>();
        services.AddScoped<RejeitarMinutaContratoUseCase>();
        services.AddScoped<AssinarContratoUseCase>();
        services.AddScoped<DisponibilizarParaAssinaturaUseCase>();
        services.AddScoped<BuscarContratoUseCase>();
        //Auth
        services.AddScoped<LoginUseCase>();

        return services;
    }
}
