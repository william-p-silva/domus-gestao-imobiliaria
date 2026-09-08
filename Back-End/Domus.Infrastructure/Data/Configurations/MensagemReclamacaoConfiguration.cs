using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Configurations;

public class MensagemReclamacaoConfiguration : IEntityTypeConfiguration<MensagemReclamacao>
{
    public void Configure(EntityTypeBuilder<MensagemReclamacao> builder)
    {
        builder.ToTable("MensagensReclamacao");

        builder.HasKey(mr => mr.MensagemReclamacao_ID);

        builder.Property(mr => mr.Texto)
            .IsRequired()
            .HasMaxLength(3000);

        builder.HasOne(mr => mr.Emissor)
            .WithMany(e => e.MensagensReclamacao)
            .HasForeignKey(mr => mr.Emissor_ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.Reclamacao)
            .WithMany(r => r.MensagensReclamacao)
            .HasForeignKey(mr => mr.Reclamacao_ID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
