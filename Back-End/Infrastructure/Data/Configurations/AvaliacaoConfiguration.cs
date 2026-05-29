

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.ToTable("Avaliacoes");

        builder.HasKey(a => a.Avaliacao_ID);

        builder.Property(a => a.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Descricao)
            .IsRequired()
            .HasMaxLength(3000);

        builder.Property(a => a.Nota)
            .IsRequired();

        builder.Property(a => a.PublicadoEm)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(a => a.Usuario)
            .WithMany(u => u.Avaliacoes)
            .HasForeignKey(a => a.Usuario_ID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Imovel)
            .WithMany(i => i.Avaliacoes)
            .HasForeignKey(a => a.Imovel_ID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Contrato)
            .WithMany()
            .HasForeignKey(a => a.Contrato_ID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
