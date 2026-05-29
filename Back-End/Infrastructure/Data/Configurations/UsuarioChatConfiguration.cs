
using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class UsuarioChatConfiguration : IEntityTypeConfiguration<UsuarioChat>
{
    public void Configure(EntityTypeBuilder<UsuarioChat> builder)
    {
        builder.ToTable("UsuarioChat");

        builder.HasKey(uc => uc.UsuarioChat_ID);

        builder.HasOne(uc => uc.Usuario)
            .WithMany(u => u.UsuarioChats)
            .HasForeignKey(uc => uc.Usuario_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(uc => uc.Chat)
            .WithMany(c => c.UsuarioChats)
            .HasForeignKey(uc => uc.Chat_ID)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
