using Domus.Domain.Entity;
using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class ReciboPagamento
{
    public Guid ReciboPagamento_ID { get; private set; }
    public Guid ParcelaAluguel_ID { get; private set; }
    public decimal ValorParcela { get; private set; }
    public string UrlRecibo { get; private set; }
    public StatusRecibo Status { get; private set; }
    public DateTime DataEmissao { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public ParcelaAluguel ParcelaAluguelRecibo { get; private set; }

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected ReciboPagamento() { }

    public ReciboPagamento(Guid parcelaAluguelId, decimal valorParcela, string urlRecibo)
    {
        if (parcelaAluguelId == Guid.Empty)
            throw new ArgumentException("O ID da parcela de aluguel não pode ser vazio.", nameof(parcelaAluguelId));
        if (valorParcela <= 0)
            throw new ArgumentException("O valor da parcela deve ser maior que zero.", nameof(valorParcela));
        if (string.IsNullOrWhiteSpace(urlRecibo))
            throw new ArgumentException("A URL do recibo não pode ser vazia.", nameof(urlRecibo));

        ReciboPagamento_ID = Guid.NewGuid();
        ParcelaAluguel_ID = parcelaAluguelId;
        ValorParcela = valorParcela;
        UrlRecibo = urlRecibo;
        Status = StatusRecibo.Emitido;
    }
}