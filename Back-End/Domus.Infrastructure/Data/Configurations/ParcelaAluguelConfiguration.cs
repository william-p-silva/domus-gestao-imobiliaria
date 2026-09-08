
using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ParcelaAluguelConfiguration : IEntityTypeConfiguration<ParcelaAluguel>
{
    public void Configure(EntityTypeBuilder<ParcelaAluguel> builder)
    {
        builder.ToTable("ParcelasAluguel");

        builder.HasKey(p => p.ParcelaAluguel_ID);

        builder.Property(p => p.ValorParcela)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.StatusPagamento)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.PixCopiaCola)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.UrlParcelaAluguel)
            .HasMaxLength(100);

        builder.Property(p => p.DataVencimento)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(p => p.DataPagamento)
            .HasColumnType("datetime2");

        builder.HasOne(p => p.Contrato)
            .WithMany(c => c.ParcelasAluguel)
            .HasForeignKey(p => p.Contrato_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
