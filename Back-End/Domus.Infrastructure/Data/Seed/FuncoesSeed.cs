
using Domus.Domain.Entity;
using Domus.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Seed;

public class FuncoesSeed : IEntityTypeConfiguration<Funcao>
{
    public void Configure(EntityTypeBuilder<Funcao> builder)
    {
        builder.HasData(
            new Funcao(Guid.Parse("11111111-1111-1111-1111-111111111111"), FuncaoUser.Administrador),
            new Funcao(Guid.Parse("22222222-2222-2222-2222-222222222222"), FuncaoUser.Locador),
            new Funcao(Guid.Parse("33333333-3333-3333-3333-333333333333"), FuncaoUser.Locatario)
        );
    }
}


