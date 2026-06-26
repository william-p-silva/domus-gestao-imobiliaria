

using Domus.Application.Interfaces.Security;
using Domus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domus.Infrastructure.Data.Seed;

public class UsuarioSeed : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasData(
            new Usuario(usuario_id: Guid.Parse("22222222-2222-2222-2222-222222222222"),
            nome: "Locador", email: "locador@domus.com", senha: passwordHasher.GerarHash("123")),

            new Usuario(usuario_id: Guid.Parse("33333333-3333-3333-3333-333333333333"),
            nome: "locatario", email: "locatario@domus.com", senha: passwordHasher.GerarHash("123")),

            new Usuario(usuario_id: Guid.Parse("11111111-1111-1111-1111-111111111111"),
            nome: "Administrador", email: "admin@domus.com", senha: passwordHasher.GerarHash("123"))
            );
    }
}
