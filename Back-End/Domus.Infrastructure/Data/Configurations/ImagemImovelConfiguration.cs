

using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ImagemImovelConfiguration : IEntityTypeConfiguration<ImagemImovel>
{
    public void Configure(EntityTypeBuilder<ImagemImovel> builder)
    {
        builder.ToTable("ImagensImovel");

        builder.HasKey(i => i.ImagemImovel_ID);

        builder.Property(i => i.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.UrlImagem)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(i => i.Imovel)
            .WithMany(im => im.Imagens)
            .HasForeignKey(i => i.Imovel_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
