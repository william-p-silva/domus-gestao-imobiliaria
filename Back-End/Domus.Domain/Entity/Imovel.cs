
using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Imovel
{
    public Guid Imovel_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Endereco_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public TipoImovel Tipo { get; private set; } 
    public decimal MetrosQuadrados { get; private set; }
    public int Comodos { get; private set; }
    public int Banheiros { get; private set; }
    public StatusImovel Status { get; private set; }
    public decimal ValorAluguel { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public bool Aprovado { get; private set; } = false;
    public bool Avaliado { get; private set; } = false;
    public DateTime? ExcluidoEm { get; private set; }

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


    public void AlterarTitulo(string novoTitulo)
    {
        if (string.IsNullOrWhiteSpace(novoTitulo))
            throw new ArgumentException("O título do imóvel é obrigatório.", nameof(novoTitulo));
        Titulo = novoTitulo;
    }


    public void AlterarDescricao(string novaDescricao)
    {
        if (string.IsNullOrWhiteSpace(novaDescricao))
            throw new ArgumentException("A descrição do imóvel é obrigatória.", nameof(novaDescricao));
        Descricao = novaDescricao;
    }


    public void AlterarTipo(TipoImovel novoTipo)
    {
        Tipo = novoTipo;
    }


    public void AlterarMetrosQuadrados(decimal novaMetragem)
    {
        if (novaMetragem <= 0)
            throw new ArgumentException("A metragem deve ser maior que zero.", nameof(novaMetragem));
        MetrosQuadrados = novaMetragem;
    }


    public void AlterarComodos(int novoNumeroComodos)
    {
        if (novoNumeroComodos <= 0)
            throw new ArgumentException("O número de cômodos deve ser maior que zero.", nameof(novoNumeroComodos));
        Comodos = novoNumeroComodos;
    }


    public void AlterarBanheiros(int novoNumeroBanheiros)
    {
        if (novoNumeroBanheiros <= 0)
            throw new ArgumentException("O número de banheiros deve ser maior que zero.", nameof(novoNumeroBanheiros));
        Banheiros = novoNumeroBanheiros;
    }


    public void AlterarValorAluguel(decimal novoValorAluguel)
    {
        if (novoValorAluguel <= 0)
            throw new ArgumentException("O valor do aluguel deve ser maior que zero.", nameof(novoValorAluguel));
        ValorAluguel = novoValorAluguel;
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
        ExcluidoEm = DateTime.UtcNow;
    }

    private void VerificarContratoAtivo()
    {
        if (Contratos.Any(c => c.Status == StatusContrato.Ativo))
            throw new InvalidOperationException("O imóvel possui contrato ativo.");
    }


    public void Alugar()
    {
        if (Status == StatusImovel.Excluido)
            throw new InvalidOperationException("Não é possível alugar um imóvel excluído.");
        if (Status == StatusImovel.Alugado)
            throw new InvalidOperationException("O imóvel já está alugado.");
        if (Status == StatusImovel.Indisponivel)
            throw new InvalidOperationException("O imóvel está indisponível para aluguel.");

        Status = StatusImovel.Alugado;
    }


    public void Disponibilizar()
    {
        VerificarContratoAtivo();
        if (Status == StatusImovel.Excluido)
            throw new InvalidOperationException("Não é possível disponibilizar um imóvel excluído.");
        if (Status == StatusImovel.Alugado)
            throw new InvalidOperationException("O imóvel está alugado e não pode ser disponibilizado.");
        if (Status == StatusImovel.Disponivel)
            throw new InvalidOperationException("O imóvel já está disponível para aluguel.");

        Status = StatusImovel.Disponivel;
    }


    public void Indisponibilizar()
    {
        VerificarContratoAtivo();
        if (Status == StatusImovel.Excluido)
            throw new InvalidOperationException("Não é possível indisponibilizar um imóvel excluído.");
        if (Status == StatusImovel.Alugado)
            throw new InvalidOperationException("O imóvel está alugado e não pode ser indisponibilizado.");
        if (Status == StatusImovel.Indisponivel)
            throw new InvalidOperationException("O imóvel já está indisponível para aluguel.");

        Status = StatusImovel.Indisponivel;
    }


    public void VerificarProprietario(Guid usuario_id)
    {
        if (Usuario_ID != usuario_id)
            throw new InvalidOperationException("O usuário não é o proprietário do imóvel.");
    }

}
