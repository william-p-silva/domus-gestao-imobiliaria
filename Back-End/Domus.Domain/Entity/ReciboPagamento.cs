using Domus.Domain.Entity;

namespace Domus.Domain;

public class ReciboPagamento
{
    public Guid ReciboPagamento_ID { get; private set; }
    public Guid ParcelaAluguel { get; private set; }
    public decimal ValorParcela { get; private set; }
    public string UrlRecibo { get; private set; }
    public StatusRecibo StatusRecibo { get; private set; }
    public DateTime DataEmissao { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public ParcelaAluguel ParcelaAluguelRecibo { get; private set; }
}