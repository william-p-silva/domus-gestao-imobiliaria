

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ReclamacaoConfiguration : IEntityTypeConfiguration<Reclamacao>
{
    public void Configure(EntityTypeBuilder<Reclamacao> builder)
    {
        builder.ToTable("Reclamacoes");

        builder.HasKey(r => r.Reclamacao_ID);

        builder.Property(r => r.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.DataInicio)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(r => r.DataResolucao)
            .HasColumnType("datetime2");

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(r => r.Usuario)
            .WithMany(u => u.Reclamacoes)
            .HasForeignKey(r => r.Usuario_ID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Imovel)
            .WithMany(i => i.Reclamacoes)
            .HasForeignKey(r => r.Imovel_ID)
            .OnDelete(DeleteBehavior.Restrict);      

    }
}
