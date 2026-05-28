
namespace Domus.Domain.Entity;

public class ParcelaAluguel
{
    public Guid ParcelaAluguel_ID { get; private set; }
    public Guid Contrato_ID { get; private set; }
    public decimal ValorParcela { get; private set; }
    public StatusPagamento StatusPagamento { get; private set; } = StatusPagamento.Pendente;
    public string PixCopiaCola { get; private set; }
    public string UrlParcelaAluguel { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public DateTime? DataPagamento { get; private set; }

    //Relacionamentos
    public Contrato Contrato { get; private set; }
    public ReciboPagamento RecibosPagamentos { get; private set; }
}
