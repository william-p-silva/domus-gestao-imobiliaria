
using Domus.Domain.Entity;
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

        builder.Property(uc => uc.ChatNome)
            .HasConversion(
                nomeVO => nomeVO.Nome,
                dbValue => NomeChat.Create(dbValue))
            .HasColumnName("NomeChat")
            .IsRequired()
            .HasMaxLength(150);

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

    }
}
