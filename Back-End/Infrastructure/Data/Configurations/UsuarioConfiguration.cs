using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Domus.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Usuario_ID);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired(false)
            .HasMaxLength(150);
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.CPF)
            .IsRequired(false)
            .HasMaxLength(11);
        builder.HasIndex(u => u.CPF)
            .IsUnique();

        builder.Property(u => u.Celular)
            .IsRequired(false)
            .HasMaxLength(11);
        builder.HasIndex(u => u.Celular)
            .IsUnique();

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Ativo)
            .IsRequired();

        builder.Property(u => u.TokenConfirmaEmail)
            .IsRequired();

        builder.Property(u => u.EmailAConfirmar)
            .IsRequired()
            .HasMaxLength(150);
        builder.HasIndex(u => u.EmailAConfirmar)
            .IsUnique();

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
