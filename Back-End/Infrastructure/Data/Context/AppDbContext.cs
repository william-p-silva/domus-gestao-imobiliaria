
using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Funcao> Funcoes { get; set; }
    public DbSet<ImagemImovel> ImagensImovel { get; set; }
    public DbSet<Imovel> Imoveis { get; set; }
    public DbSet<MensagemChat> MensagensChat { get; set; }
    public DbSet<MensagemReclamacao> MensagensReclamacao { get; set; }
    public DbSet<Notificacao> Notificacoes { get; set; }
    public DbSet<ParcelaAluguel> ParcelasAluguel { get; set; }
    public DbSet<ReciboPagamento> RecibosPagamentos { get; set; }
    public DbSet<Reclamacao> Reclamacoes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioChat> UsuariosChat { get; set; }
    public DbSet<UsuarioFuncao> UsuarioFuncoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esta linha faz a mágica: busca todas as classes que 
        // implementam IEntityTypeConfiguration neste assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
