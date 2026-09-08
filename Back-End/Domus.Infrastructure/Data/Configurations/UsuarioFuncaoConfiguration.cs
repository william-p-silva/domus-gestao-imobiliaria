
using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class UsuarioFuncaoConfiguration : IEntityTypeConfiguration<UsuarioFuncao>
{
    public void Configure(EntityTypeBuilder<UsuarioFuncao> builder)
    {
        builder.ToTable("UsuarioFuncoes");

        builder.HasKey(uf => uf.UsuarioFuncao_ID);

        builder.Property(uf => uf.CriadoEm)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(uf => uf.Funcao)
            .WithMany(f => f.UsuarioFuncao)
            .HasForeignKey(uf => uf.Funcao_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(uf => uf.Usuario)
            .WithMany(u => u.UsuarioFuncao)
            .HasForeignKey(uf => uf.Usuario_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
