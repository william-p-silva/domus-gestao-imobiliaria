

using Domus.Domain.Entity;
using Domus.Domain.ValueObjects.Caracteristica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class GrupoCaracteristicaConfiguration : IEntityTypeConfiguration<GrupoCaracteristica>
{
    public void Configure(EntityTypeBuilder<GrupoCaracteristica> builder)
    {
        builder.ToTable("GruposCaracteristica");

        builder.HasKey(g => g.GrupoCaracteristica_ID);

        builder.Property(g => g.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.Codigo)
            .HasConversion(
                codigo => codigo.Value,
                value => CodigoGrupoCaracteristica.Create(value))
            .HasColumnName("Codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(g => g.Codigo)
            .IsUnique();

        builder.Property(g => g.Estado)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(g => g.DeletadoEm)
            .IsRequired(false);

        builder.Property(g => g.CriadoEm)
            .IsRequired();
    }
}
