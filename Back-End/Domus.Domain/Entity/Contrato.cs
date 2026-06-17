

using Domus.Domain.Enums;
using System.Diagnostics.Contracts;

namespace Domus.Domain.Entity;

public class Contrato
{
    public Guid Contrato_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public Guid Locador_ID { get; private set; }
    public Guid? Locatario_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Tipo { get; private set; }
    public string UrlContrato { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataTermino { get; private set; }
    public StatusContrato Status { get; private set; }
    public bool AssinaturaLocador { get; private set; } = false;
    public bool AssinaturaLocatario { get; private set; } = false;

    //Relacionamentos
    public List<ParcelaAluguel> ParcelasAluguel { get; private set; } = new List<ParcelaAluguel>();
    public Imovel Imovel { get; private set; }
    public Usuario Locador { get; private set; }
    public Usuario Locatario { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Contrato() { }


    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Contrato"/> como um rascunho (minuta), 
    /// garantindo as validações obrigatórias e que o locador seja o proprietário do imóvel.
    /// </summary>
    /// <param name="imovel_id">O identificador exclusivo do imóvel.</param>
    /// <param name="locador_id">O identificador exclusivo do locador/proprietário.</param>
    /// <param name="titulo">O título descritivo do contrato.</param>
    /// <param name="descricao">Os detalhes e cláusulas do contrato.</param>
    /// <param name="tipo">O tipo de contrato (ex: Residencial, Comercial).</param>
    /// <param name="urlContrato">O endereço de armazenamento do documento físico ou PDF.</param>
    /// <param name="imovel">A instância opcional do <see cref="Imovel"/> para validação de vínculo com o locador.</param>
    /// <exception cref="ArgumentException">
    /// Lançada se qualquer um dos campos obrigatórios estiver vazio, nulo ou inválido, 
    /// ou se o <paramref name="locador_id"/> não for o proprietário do <paramref name="imovel"/> informado.
    /// </exception>
    public Contrato
        (Guid imovel_id, Guid locador_id, string titulo, string descricao,
          string tipo, string urlContrato, Imovel imovel)
    {
        if (imovel_id == Guid.Empty)
            throw new ArgumentException("O ID do imóvel é obrigatório.", nameof(imovel_id));
        if (locador_id == Guid.Empty)
            throw new ArgumentException("O ID do locador é obrigatório.", nameof(locador_id));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do contrato é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição do contrato é obrigatória.", nameof(descricao));
        if (string.IsNullOrWhiteSpace(tipo))
            throw new ArgumentException("O tipo do contrato é obrigatório.", nameof(tipo));
        if (string.IsNullOrWhiteSpace(urlContrato))
            throw new ArgumentException("A URL do contrato é obrigatória.", nameof(urlContrato));

        if (imovel != null && imovel.Usuario_ID != locador_id)
            throw new ArgumentException("O locador deve ser o proprietário do imóvel.", nameof(locador_id));

        Contrato_ID = Guid.NewGuid();
        Imovel_ID = imovel_id;
        Locador_ID = locador_id;
        Titulo = titulo;
        Descricao = descricao;
        Tipo = tipo;
        UrlContrato = urlContrato;
        Status = StatusContrato.Rascunho;
    }


    /// <summary>
    /// Registra a assinatura do locador na minuta e disponibiliza o contrato para a análise e assinatura do locatário.
    /// </summary>
    /// <param name="locatario_id">O identificador exclusivo (<see cref="Guid"/>) do candidato a inquilino.</param>
    /// <exception cref="ArgumentException">Lançada caso o <paramref name="locatario_id"/> seja um Guid vazio.</exception>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o contrato não estiver em <see cref="StatusContrato.Rascunho"/>, 
    /// se o locador já tiver assinado, ou se a minuta já possuir um locatário vinculado.
    /// </exception>
    public void LocadorDisponibilizaAssinaturaMinuta(Guid locatario_id)
    {
        if (locatario_id == Guid.Empty)
            throw new ArgumentException("Locatario invalido ", nameof(locatario_id));

        if (Status != StatusContrato.Rascunho)
            throw new InvalidOperationException("O contrato só pode ser disponibilizado para assinatura se estiver em rascunho.");
        if (AssinaturaLocador == true)
            throw new InvalidOperationException("Contrato já assinado");
        if (Locatario_ID != Guid.Empty || Locatario_ID == null)
            throw new InvalidOperationException("Já existe um locatario para este contrato ");

        Locatario_ID = locatario_id;
        AssinaturaLocador = true;
        Status = StatusContrato.Pendente;
    }

    /// <summary>
    /// Registra a assinatura do locatário na minuta e engatilha a ativação automática do contrato de locação.
    /// </summary>
    /// <param name="dataTermino">A data combinada para o término do contrato.</param>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o contrato não estiver com o status <see cref="StatusContrato.Pendente"/>, 
    /// se o locatário já tiver assinado, ou se não houver nenhum locatário devidamente vinculado à minuta.
    /// </exception>
    public void LocatarioAssinaMinuta(DateTime dataTermino, decimal valorAluguel)
    {
        if (Status != StatusContrato.Pendente)
            throw new InvalidOperationException("O contrato só pode ser assinado pelo locatario caso ele esteja como pendente.");
        if (AssinaturaLocatario == true)
            throw new InvalidOperationException("Contrato já assinado");
        if (Locatario_ID == Guid.Empty || Locatario_ID == null)
            throw new InvalidOperationException("Não existe um locatario para este contrato");

        AssinaturaLocatario = true;
        AtivarContrato(dataTermino: dataTermino, valorAluguel);
    }


    /// <summary>
    /// Executa a transição interna de estado para ativar o contrato, cravando a data de início e definindo o encerramento da vigência.
    /// </summary>
    /// <param name="dataTermino">A data final combinada para o encerramento do contrato de locação.</param>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o contrato não estiver com o status <see cref="StatusContrato.Pendente"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Lançada se a <paramref name="dataTermino"/> não respeitar o período mínimo exigido de vigência (menor ou igual a um mês a partir de hoje).
    /// </exception>
    private void AtivarContrato(DateTime dataTermino, decimal valorAluguel)
    {
        if (Status != StatusContrato.Pendente)
            throw new InvalidOperationException("O contrato só poder ser ativado caso esteja pendente");

        if (dataTermino <= DateTime.UtcNow.AddMonths(1))
            throw new ArgumentException("A data de término deve ser de no mínimo um mês a partir de hoje.");

        DataInicio = DateTime.UtcNow;
        DataTermino = dataTermino;
        Status = StatusContrato.Ativo;

        GerarParcelasContrato(valorAluguel: valorAluguel);
    }

    /// <summary>
    /// Cancela a pendência de assinatura da minuta, limpando os vínculos e retornando o contrato para o estado editável de rascunho.
    /// Pode ser executado tanto pelo locador quanto pelo locatário antes da assinatura final.
    /// </summary>
    /// <param name="usuarioId">ID do usuário que está solicitando o cancelamento.</param>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o contrato não estiver com o status <see cref="StatusContrato.Pendente"/> 
    /// ou se o usuário não fizer parte do contrato.
    /// </exception>
    public void CancelarPendenciaMinuta(Guid usuario_ID)
    {
        if (Status != StatusContrato.Pendente)
            throw new InvalidOperationException("O contrato só poder ser rejeitado caso esteja pendente");

        if (Locatario_ID != usuario_ID && Locador_ID != usuario_ID)
            throw new ArgumentException("Usuário sem permissão ", nameof(usuario_ID));

        Status = StatusContrato.Rascunho;
        AssinaturaLocador = false;
        AssinaturaLocatario = false;
        Locatario_ID = null;
    }


    private void GerarParcelasContrato(decimal valorAluguel)
    {
        if (valorAluguel <= 0)
            throw new InvalidOperationException("Valor do aluguel incoerente ");

        int mesesContrato = ((DataTermino.Value.Year - DataInicio.Value.Year) * 12 ) + 
                                DataTermino.Value.Month - DataInicio.Value.Month;

        if (mesesContrato <= 0) mesesContrato = 1;

        for (int i = 1; i <= mesesContrato; i++)
        {
            var dataVencimento = DataInicio.Value.AddMonths(i);

            var novaParcelaAluguel = new ParcelaAluguel(
                contratoId: Contrato_ID,
                valorParcela: valorAluguel,
                urlParcelaAluguel: $"https://domus.com/faturas/{Contrato_ID}/{i}",
                dataVencimento: dataVencimento,
                descricao: $"Parcela de Aluguel {i}/{mesesContrato} - {Titulo}",
                pixCopiaCola: $"PIX_FAKE_CHAVE_CONTRATO_{Contrato_ID}_PARCELA_{i}" // Substitua pela sua lógica/serviço de Pix real futuramente
                );

            ParcelasAluguel.Add(novaParcelaAluguel);
        }
    }
}
