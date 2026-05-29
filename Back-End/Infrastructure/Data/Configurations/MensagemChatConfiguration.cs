


using Domus.Domain.Entity;
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

        builder.Property(mc => mc.DataEnvio)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(mc => mc.Usuario)
            .WithMany(u => u.MensagensChat)
            .HasForeignKey(mc => mc.Usuario_ID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mc => mc.Chat)
            .WithMany(c => c.MensagensChat)
            .HasForeignKey(mc => mc.Chat_ID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
