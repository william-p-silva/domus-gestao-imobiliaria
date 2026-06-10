using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable("Contratos");

        builder.HasKey(c => c.Contrato_ID);

        builder.Property(c => c.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(3000);

        builder.Property(c => c.Tipo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.UrlContrato)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(c => c.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(c => c.DataInicio)
            .IsRequired(false)
            .HasColumnType("datetime2");

        builder.Property(c => c.DataTermino)
            .IsRequired(false)
            .HasColumnType("datetime2");

        builder.Property(c => c.AssinaturaLocador)
            .IsRequired()
            .HasDefaultValue(false);


        builder.Property(c => c.AssinaturaLocatario)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(c => c.Imovel)
            .WithMany(i => i.Contratos)
            .HasForeignKey(c => c.Imovel_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Locador)
            .WithMany(u => u.ContratosComoLocador)
            .HasForeignKey(c => c.Locador_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Locatario)
            .WithMany(u => u.ContratosComoLocatario)
            .IsRequired(false)
            .HasForeignKey(c => c.Locatario_ID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
