


using Domus.Domain.Entity;
using Domus.Domain.Enums.Chat;
using Domus.Domain.Enums.Mensagem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class MensagemChatConfiguration : IEntityTypeConfiguration<MensagemChat>
{
    public void Configure(EntityTypeBuilder<MensagemChat> builder)
    {
        builder.ToTable("MensagensChat");

        builder.HasKey(mc => mc.MensagemChat_ID);

        builder.Property(mc => mc.Texto)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(m => m.Estado)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(m => m.DeletadaEm)
            .IsRequired(false);

        builder.Property(mc => mc.DataEnvio)
            .IsRequired();

        builder.HasOne(mc => mc.UsuarioChat)
            .WithMany(u => u.MensagensChat)
            .HasForeignKey(mc => mc.UsuarioChat_ID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mc => mc.Chat)
            .WithMany(c => c.MensagensChat)
            .HasForeignKey(mc => mc.Chat_ID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(
            m => m.Estado != EstadoMensagem.Apagada &&
            m.UsuarioChat.Estado != EstadoUsuarioChat.Deletado
            );
    }
}
