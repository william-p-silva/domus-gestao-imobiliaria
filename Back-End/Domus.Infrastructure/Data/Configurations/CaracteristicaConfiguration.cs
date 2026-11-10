

using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class CaracteristicaConfiguration : IEntityTypeConfiguration<Caracteristica>
{
    public void Configure(EntityTypeBuilder<Caracteristica> builder)
    {
        builder.ToTable("Caracteristicas");

        builder.HasKey(c => c.Caracteristica_ID);

        builder.ComplexProperty(c => c.Nome, nomeBuilder =>
        {
            nomeBuilder.Property(n => n.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(70)
                .IsRequired();
        });

        builder.Property(c => c.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(c => c.CriadoEm)
            .IsRequired();

        builder.Property(c => c.DeletadoEm)
            .IsRequired(false);

        builder.HasQueryFilter(c => c.Estado != EstadoCaracteristica.Inativa && c.DeletadoEm == null);
    }
}
