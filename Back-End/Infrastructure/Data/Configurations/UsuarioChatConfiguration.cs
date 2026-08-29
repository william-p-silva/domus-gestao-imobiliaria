
using Domus.Domain.Entity;
using Domus.Domain.Enums.Chat;
using Domus.Domain.ValueObjects.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class UsuarioChatConfiguration : IEntityTypeConfiguration<UsuarioChat>
{
    public void Configure(EntityTypeBuilder<UsuarioChat> builder)
    {
        builder.ToTable("UsuarioChat");

        builder.HasKey(uc => uc.UsuarioChat_ID);

        builder.ComplexProperty(uc => uc.ChatNome, nomeBuilder =>
        {
            nomeBuilder.Property(n => n.Nome)
            .HasColumnName("NomeChat")
            .HasMaxLength(150)
            .IsRequired();
        });

        builder.Property(uc => uc.Funcao)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(uc => uc.CriadoEm)
            .IsRequired();

        builder.Property(uc => uc.DeletadoEm)
            .IsRequired(false);

        builder.Property(uc => uc.Estado)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(uc => uc.Usuario)
            .WithMany(u => u.UsuarioChats)
            .HasForeignKey(uc => uc.Usuario_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(uc => uc.Chat)
            .WithMany(c => c.UsuarioChats)
            .HasForeignKey(uc => uc.Chat_ID)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasQueryFilter(uc => uc.Estado != EstadoUsuarioChat.Deletado);

    }
}
