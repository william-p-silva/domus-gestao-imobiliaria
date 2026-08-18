

export interface EnderecoResponse {
    endereco_id: string,
    cep: string,
    uf: string,
    cidade: string,
    bairro: string,
    rua: string,
    numero: string,
    complemento?: string
}