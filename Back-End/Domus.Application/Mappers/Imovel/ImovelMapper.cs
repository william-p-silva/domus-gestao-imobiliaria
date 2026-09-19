

using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Domain.Entity;
using Domus.Domain.Enums;

namespace Domus.Application.Mappers.Imovel;

public static class ImovelMapper
{
    public static ImovelResponse ToResponse(this Domain.Entity.Imovel imovel)
    {
        var contrato = ObterContratoRelevante(imovel.Contratos);

        return new ImovelResponse
        {
            Imovel_ID = imovel.Imovel_ID,
            Titulo = imovel.Titulo,
            Descricao = imovel.Descricao,
            Comodos = imovel.Comodos,
            Status = imovel.Status.ToString(),
            ValorAluguel = imovel.ValorAluguel,
            CriadoEm = imovel.CriadoEm,
            Aprovado = imovel.Aprovado,
            Avaliado = imovel.Avaliado,
            MetrosQuadrados = imovel.MetrosQuadrados,
            Banheiros = imovel.Banheiros,
            TipoDoImovel = imovel.Tipo.ToString(),
            Endereco = new EnderecoResponse
            {
                Endereco_ID = imovel.Endereco_ID,
                CEP = imovel.Endereco.CEP,
                UF = imovel.Endereco.UF,
                Cidade = imovel.Endereco.Cidade,
                Bairro = imovel.Endereco.Bairro,
                Rua = imovel.Endereco.Rua,
                Numero = imovel.Endereco.Numero,
                Complemento = imovel.Endereco.Complemento
            },
            Locador = new ResponseUsuarioImovel
            {
                Usuario_ID = imovel.Usuario.Usuario_ID,
                Email = imovel.Usuario.Email.Endereco,
                Nome = imovel.Usuario.Nome.NomeCompleto
            },
            Contrato = contrato != null ? new ResponseContratoImovel
            {
                Contrato_ID = contrato.Contrato_ID,
                CriadoEm = contrato.CriadoEm,
                Status = contrato.Status.ToString(),
                Descricao = contrato.Descricao,
                Tipo = contrato.Tipo,
                Titulo = contrato.Titulo,
                UrlContrato = contrato.UrlContrato
            } : null
        };
    }

    private static Domain.Entity.Contrato? ObterContratoRelevante(IEnumerable<Domain.Entity.Contrato> contratos)
    {
        if (contratos == null) return null;

        return contratos.FirstOrDefault(c =>
            c.Status == StatusContrato.Ativo 
            || c.Status == StatusContrato.Rascunho 
            ||c.Status == StatusContrato.Pendente
        );
    }
}
