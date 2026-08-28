import type { EnderecoResponse } from "../endereco/enderecoResponse";



export interface ImovelResponse {
    imovel_ID: string,
    usuario_ID: string,
    titulo: string,
    descricao: string,
    comodos: number,
    status: string,
    valorAluguel: number,
    criadoEm: string,
    aprovado: string,
    avaliado: string,
    metrosQuadrados: string,
    banheiros: string,
    tipoDoImovel: string,
    endereco: EnderecoResponse
}

