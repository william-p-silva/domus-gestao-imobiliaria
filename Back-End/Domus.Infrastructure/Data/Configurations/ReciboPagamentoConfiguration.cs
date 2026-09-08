
using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ReciboPagamentoConfiguration : IEntityTypeConfiguration<ReciboPagamento>
{
    public void Configure(EntityTypeBuilder<ReciboPagamento> builder)
    {
        builder.ToTable("RecibosPagamento");

        builder.HasKey(r => r.ReciboPagamento_ID);

        builder.Property(r => r.ValorParcela)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(r => r.UrlRecibo)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(r => r.DataEmissao)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(r => r.ParcelaAluguelRecibo)
            .WithMany(p => p.RecibosPagamento)
            .HasForeignKey(r => r.ParcelaAluguel_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
