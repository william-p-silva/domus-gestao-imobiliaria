

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

internal class CaracteristicaImovelConfiguration : IEntityTypeConfiguration<CaracteristicaImovel>
{
    public void Configure(EntityTypeBuilder<CaracteristicaImovel> builder)
    {
        builder.ToTable("CaracteristicasImovel");

        builder.HasKey(ci => ci.CaracteristicaImovel_ID);

        builder.Property(ci => ci.CriadoEm)
            .IsRequired();

        builder.Property(ci => ci.DeletadoEm)
            .IsRequired(false);

        builder.HasOne(ci => ci.Imovel)
            .WithMany(i => i.CaracteristicasImovel)
            .HasForeignKey(ci => ci.Imovel_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ci => ci.Caracteristica)
            .WithMany(c => c.CaracteristicasImovel)
            .HasForeignKey(ci => ci.Caracteristica_ID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
