

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class FuncaoConfiguration : IEntityTypeConfiguration<Funcao>
{
    public void Configure(EntityTypeBuilder<Funcao> builder)
    {
        builder.ToTable("Funcoes");

        builder.HasKey(f => f.Funcao_ID);

        builder.Property(f => f.Nome)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

    }
}
