

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(e => e.Endereco_ID);

        builder.Property(e => e.CEP)
            .IsRequired()
            .HasColumnType("char(8)");

        builder.Property(e => e.UF)
            .IsRequired()
            .HasColumnType("char(2)");

        builder.Property(e => e.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Bairro)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Rua)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Complemento)
            .HasMaxLength(200);
    }
}
