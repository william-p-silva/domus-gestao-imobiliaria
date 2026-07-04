
using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Imovel
{
    public Guid Imovel_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Endereco_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public TipoImovel Tipo { get; private set; } //
    public decimal MetrosQuadrados { get; private set; }//
    public int Comodos { get; private set; }
    public int Banheiros { get; private set; }//
    public StatusImovel Status { get; private set; }
    public decimal ValorAluguel { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public bool Aprovado { get; private set; } = false;
    public bool Avaliado { get; private set; } = false;

    //Relacionamentos
    public ICollection<Reclamacao> Reclamacoes { get; private set; } = new List<Reclamacao>();
    public ICollection<Contrato> Contratos { get; private set; } = new List<Contrato>();

    public ICollection<ImagemImovel> Imagens { get; private set; } = new List<ImagemImovel>();
    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();

    public ICollection<Chat> Chats { get; private set; } = new List<Chat>();

    public Usuario Usuario { get; private set; }
    public Endereco Endereco { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Imovel() { }

    public Imovel(
        Guid usuario_id,
        Guid endereco_id,
        string titulo,
        string descricao,
        int comodos,
        StatusImovel status,
        decimal valorAluguel,
        int banheiros,
        TipoImovel tipo,
        decimal metrosQuadrados
        )
    {
        if (usuario_id == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(usuario_id));
        if (endereco_id == Guid.Empty)
            throw new ArgumentException("O ID do endereço é obrigatório.", nameof(endereco_id));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do imóvel é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição do imóvel é obrigatória.", nameof(descricao));
        if (metrosQuadrados <= 0)
            throw new ArgumentException("A metragem deve ser maior que zero", nameof(metrosQuadrados));
        if (comodos <= 0)
            throw new ArgumentException("O número de cômodos deve ser maior que zero.", nameof(comodos));
        if(banheiros <= 0)
            throw new ArgumentException("O número de Banheiros deve ser maior que zero.", nameof(banheiros));
        if (valorAluguel <= 0)
            throw new ArgumentException("O valor do aluguel deve ser maior que zero.", nameof(valorAluguel));

        Imovel_ID = Guid.NewGuid();
        Usuario_ID = usuario_id;
        Endereco_ID = endereco_id;
        Titulo = titulo;
        Descricao = descricao;
        Tipo = tipo;
        MetrosQuadrados = metrosQuadrados;
        Comodos = comodos;
        Banheiros = banheiros;
        Status = status;
        ValorAluguel = valorAluguel;
    }


    public void Avaliar(bool aprovado)
    {
        if(Status == StatusImovel.Excluido)
            throw new InvalidOperationException("Não é possível avaliar um imóvel excluído.");
        if(Avaliado)
            throw new InvalidOperationException("O imóvel já foi avaliado.");
        Aprovado = aprovado;
        Avaliado = true;
    }

    /// <summary>
    /// Exclui o imóvel, alterando seu status para "Excluído".
    /// </summary>
    /// <exception cref="InvalidOperationException">Status que não permite exclusão</exception>
    public void Excluir()
    {
        if (Status == StatusImovel.Excluido)
            throw new InvalidOperationException("O imóvel já está excluído.");
        if (Status == StatusImovel.Alugado)
            throw new InvalidOperationException("Não é possível excluir um imóvel alugado.");
        VerificarContratoAtivo();
        Status = StatusImovel.Excluido;
    }

    private void VerificarContratoAtivo()
    {
        if (Contratos.Any(c => c.Status == StatusContrato.Ativo))
            throw new InvalidOperationException("O imóvel possui contrato ativo.");
    }

}
