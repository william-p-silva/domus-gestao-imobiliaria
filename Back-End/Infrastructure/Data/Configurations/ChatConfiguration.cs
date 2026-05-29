

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");

        builder.HasKey(c => c.Chat_ID);

        builder.HasOne(c => c.Imovel)
            .WithMany(i => i.Chats)
            .HasForeignKey(c => c.Imovel_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
