

using Domus.Domain.Entity;
using Domus.Domain.Enums.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");

        builder.HasKey(c => c.Chat_ID);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Estado)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.DeletadoEm)
            .IsRequired(false);

        builder.Property(c => c.CriadoEm)
            .IsRequired();

        builder.HasOne(c => c.Imovel)
            .WithMany(i => i.Chats)
            .HasForeignKey(c => c.Imovel_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(c => c.Estado != EstadoChat.Deletado);
    }
}
