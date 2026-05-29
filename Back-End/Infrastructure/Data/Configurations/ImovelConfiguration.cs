

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ImovelConfiguration : IEntityTypeConfiguration<Imovel>
{
    public void Configure(EntityTypeBuilder<Imovel> builder)
    {
        builder.ToTable("Imoveis");

        builder.HasKey(i => i.Imovel_ID);

        builder.Property(i => i.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Descricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(i => i.Comodos)
            .IsRequired();

        builder.Property(i => i.ValorAluguel)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(i => i.Usuario)
            .WithMany(u => u.Imoveis)
            .HasForeignKey(i => i.Usuario_ID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Endereco)
            .WithOne()
            .HasForeignKey<Imovel>(i => i.Endereco_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
