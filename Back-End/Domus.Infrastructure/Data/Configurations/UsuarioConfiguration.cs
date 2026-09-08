using Domus.Domain.Entity;
using Domus.Domain.ValueObjects.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Domus.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Usuario_ID);

        builder.ComplexProperty(n => n.Nome, nomeBuilder =>
        {
            nomeBuilder.Property(un => un.NomeCompleto)
                .HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(150);
        });

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Endereco)
                .HasColumnName("Email")
                .HasMaxLength(200);
            
            emailBuilder.HasIndex(u => u.Endereco)
                .IsUnique()
                .HasFilter("[Email] IS NOT NULL AND [ExcluidoEm] IS NULL");
        });

        builder.OwnsOne(c => c.CPF, cpfBuilder =>
        {
            cpfBuilder.Property(cn => cn.Numero)
                .HasColumnName("CPF")
                .HasMaxLength(11);

            cpfBuilder.HasIndex(u => u.Numero)
                .IsUnique()
                .HasFilter("[CPF] IS NOT NULL AND [ExcluidoEm] IS NULL");
        });


        builder.OwnsOne(c => c.Celular, celBuilder =>
        {
            celBuilder.Property(n => n.Numero)
                .HasColumnName("Celular")
                .HasMaxLength(11);

            celBuilder.HasIndex(u => u.Numero)
                .IsUnique()
                .HasFilter("[Celular] IS NOT NULL AND [ExcluidoEm] IS NULL");
        });


        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Ativo)
            .IsRequired();

        builder.Property(u => u.ExcluidoEm)
            .IsRequired(false)
            .HasColumnType("datetime2");

        builder.Property(u => u.TokenConfirmaEmail)
            .IsRequired();

        builder.OwnsOne(u => u.EmailAConfirmar, emailBuilder =>
        {
            emailBuilder.Property(e => e.Endereco)
            .HasColumnName("EmailAConfirmar")
            .HasMaxLength(200);

            emailBuilder.HasIndex(u => u.Endereco)
                    .IsUnique()
                    .HasFilter("[ExcluidoEm] IS NULL");
        });


        builder.Property(u => u.TokenEmailExpire)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(u => u.EmailConfirmado)
            .IsRequired();

        builder.Property(u => u.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(u => u.EnderecoUsuario)
             .WithOne() // Deixe vazio se a classe Endereco não tiver uma propriedade de navegação de volta para Usuario
             .HasForeignKey<Usuario>(u => u.Endereco_ID)
             .IsRequired(false) // Define explicitamente que o relacionamento NÃO é obrigatório
             .OnDelete(DeleteBehavior.SetNull); // Se o endereço for deletado, o Endereco_ID do usuário vira NULL
    }
}
