
using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class ParcelaAluguel
{
    public Guid ParcelaAluguel_ID { get; private set; }
    public Guid Contrato_ID { get; private set; }
    public string Descricao { get; private set; }
    public decimal ValorParcela { get; private set; }
    public StatusPagamento StatusPagamento { get; private set; } = StatusPagamento.Pendente;
    public string PixCopiaCola { get; private set; }
    public string UrlParcelaAluguel { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public DateTime? DataPagamento { get; private set; }

    //Relacionamentos
    public Contrato Contrato { get; private set; }
    public ICollection<ReciboPagamento> RecibosPagamento { get; private set; } = new List<ReciboPagamento>();

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected ParcelaAluguel() { }

    public ParcelaAluguel(Guid contratoId, decimal valorParcela, string urlParcelaAluguel,
        DateTime dataVencimento, string descricao, string pixCopiaCola)
    {
        if (contratoId == Guid.Empty)
            throw new ArgumentException("O ID do contrato não pode ser vazio.", nameof(contratoId));
        if (valorParcela <= 0)
            throw new ArgumentException("O valor da parcela deve ser maior que zero.", nameof(valorParcela));
        if (string.IsNullOrWhiteSpace(pixCopiaCola))
            throw new ArgumentException("O Pix cópia e cola não pode ser vazio.", nameof(pixCopiaCola));
        if (string.IsNullOrWhiteSpace(urlParcelaAluguel))
            throw new ArgumentException("A URL da parcela de aluguel não pode ser vazia.", nameof(urlParcelaAluguel));
        if (dataVencimento <= DateTime.UtcNow)
            throw new ArgumentException("A data de vencimento deve ser futura.", nameof(dataVencimento));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da parcela de aluguel não pode ser vazia.", nameof(descricao));

        ParcelaAluguel_ID = Guid.NewGuid();
        Contrato_ID = contratoId;
        ValorParcela = valorParcela;
        PixCopiaCola = pixCopiaCola;
        UrlParcelaAluguel = urlParcelaAluguel;
        DataVencimento = dataVencimento;
        Descricao = descricao;
    }
}
