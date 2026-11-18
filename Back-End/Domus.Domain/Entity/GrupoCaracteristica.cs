
using Domus.Domain.Enums.Caracteristica;
using Domus.Domain.Exceptions.Domain;
using Domus.Domain.ValueObjects.Caracteristica;

namespace Domus.Domain.Entity;

public class GrupoCaracteristica
{
    public Guid GrupoCaracteristica_ID { get; private set; }

    public string Nome { get; private set; }
    public CodigoGrupoCaracteristica Codigo { get; private set; }

    public EstadoCaracteristica Estado { get; private set; }

    public DateTime CriadoEm { get; private set; }
    public DateTime? DeletadoEm { get; private set; }

    public ICollection<Caracteristica> Caracteristicas { get; private set; }
        = new List<Caracteristica>();

    protected GrupoCaracteristica() { }

    public GrupoCaracteristica(
        string nome, 
        string codigo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ValidationException("O nome não pode estar vazio.");
        if (nome.Length <= 3)
            throw new ValidationException("O nome não pode ser inferior a 3 caracterres.");

        GrupoCaracteristica_ID = Guid.NewGuid();
        Nome = nome;
        Codigo = CodigoGrupoCaracteristica.Create(codigo);
        Estado = EstadoCaracteristica.Ativa;
        CriadoEm = DateTime.UtcNow;
    }

    public void Ativar()
    {
        if (Estado == EstadoCaracteristica.Ativa || DeletadoEm == null)
            throw new ValidationException("Grupo de característica já está ativo");

        Estado = EstadoCaracteristica.Ativa;
        DeletadoEm = null;
    }
}