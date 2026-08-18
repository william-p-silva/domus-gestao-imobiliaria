import type { ImovelResponse } from "@/shared/types/imovel/imovelResponse";

// 1. Objeto Padrão (Cenário de Sucesso Completo)
export const mockImovelCompleto: ImovelResponse = {
    imovel_id: "550e8400-e29b-41d4-a716-446655440000",
    usuario_id: "usr-987654321",
    titulo: "Apartamento Moderno no Centro",
    descricao: "Excelente apartamento com vista definitiva, próximo ao metrô e comércio.",
    comodos: 4,
    status: "DISPONIVEL",
    valorAluguel: 2500.00,
    criadoEm: "2026-01-15T10:30:00.000Z",
    aprovado: "SIM",
    avaliado: "SIM",
    metrosQuadrados: "75.5",
    banheiros: "2",
    tipoDoImovel: "APARTAMENTO",
    endereco: {
        endereco_id: "end-123456",
        cep: "01001-000",
        uf: "SP",
        cidade: "São Paulo",
        bairro: "Sé",
        rua: "Praça da Sé",
        numero: "100",
        complemento: "Apto 802 - Bloco B"
    }
};

// 2. Objeto Opcional Ausente (Sem complemento no endereço)
export const mockImovelSemComplemento: ImovelResponse = {
    imovel_id: "6a2c9100-f19c-42a1-b816-112233445566",
    usuario_id: "usr-123456789",
    titulo: "Casa de Bairro Tranquila",
    descricao: "Casa espaçosa com quintal grande, ideal para famílias com pets.",
    comodos: 6,
    status: "ALUGADO",
    valorAluguel: 4200.50,
    criadoEm: "2026-02-01T14:20:00.000Z",
    aprovado: "SIM",
    avaliado: "SIM",
    metrosQuadrados: "150",
    banheiros: "3",
    tipoDoImovel: "CASA",
    endereco: {
        endereco_id: "end-789012",
        cep: "30130-010",
        uf: "MG",
        cidade: "Belo Horizonte",
        bairro: "Funcionários",
        rua: "Avenida Afonso Pena",
        numero: "1500"
    }
};

// 3. Objeto em Análise / Pendente (Valores iniciais e aprovação pendente)
export const mockImovelPendente: ImovelResponse = {
    imovel_id: "7b3d0200-a38b-43d2-c917-998877665544",
    usuario_id: "usr-456789123",
    titulo: "Studio Compacto",
    descricao: "Studio novo recém-entregue, ideal para estudantes.",
    comodos: 2,
    status: "EM_ANALISE",
    valorAluguel: 1200.00,
    criadoEm: "2026-08-18T08:00:00.000Z",
    aprovado: "NAO",
    avaliado: "NAO",
    metrosQuadrados: "28",
    banheiros: "1",
    tipoDoImovel: "STUDIO",
    endereco: {
        endereco_id: "end-345678",
        cep: "80010-000",
        uf: "PR",
        cidade: "Curitiba",
        bairro: "Centro",
        rua: "Rua XV de Novembro",
        numero: "450",
        complemento: "Apto 31"
    }
};

// 5. Imóvel Comercial / Sala Comercial (Para testar tipos comerciais)
export const mockImovelComercial: ImovelResponse = {
    imovel_id: "8c4e1311-b49c-54e3-d028-112233445577",
    usuario_id: "usr-001122334",
    titulo: "Sala Comercial Executive Tower",
    descricao: "Conjunto comercial moderno com recepção, piso elevado e 2 vagas de garagem.",
    comodos: 3,
    status: "DISPONIVEL",
    valorAluguel: 3800.00,
    criadoEm: "2026-03-10T11:15:00.000Z",
    aprovado: "SIM",
    avaliado: "SIM",
    metrosQuadrados: "60",
    banheiros: "2",
    tipoDoImovel: "COMERCIAL",
    endereco: {
        endereco_id: "end-901234",
        cep: "90010-270",
        uf: "RS",
        cidade: "Porto Alegre",
        bairro: "Praia de Belas",
        rua: "Avenida Borges de Medeiros",
        numero: "2500",
        complemento: "Sala 1204"
    }
};

// 6. Chácara / Imóvel Rural (Sem número tradicional e valores maiores de área)
export const mockImovelRural: ImovelResponse = {
    imovel_id: "9d5f2422-c50d-65f4-e139-223344556688",
    usuario_id: "usr-556677889",
    titulo: "Chácara Recanto Verde",
    descricao: "Chácara com casa sede de 3 quartos, piscina, campo de futebol e área gourmet.",
    comodos: 8,
    status: "DISPONIVEL",
    valorAluguel: 6500.00,
    criadoEm: "2026-04-05T09:45:00.000Z",
    aprovado: "SIM",
    avaliado: "SIM",
    metrosQuadrados: "2500",
    banheiros: "4",
    tipoDoImovel: "CHACARA",
    endereco: {
        endereco_id: "end-567890",
        cep: "13200-000",
        uf: "SP",
        cidade: "Jundiaí",
        bairro: "Caxambu",
        rua: "Estrada Municipal das Roseiras",
        numero: "S/N",
        complemento: "Km 4.5"
    }
};

// 7. Imóvel Inativo / Reprovado na Avaliação
export const mockImovelReprovado: ImovelResponse = {
    imovel_id: "0a6a3533-d61e-76a5-f240-334455667799",
    usuario_id: "usr-998877665",
    titulo: "Sobrado Antigo para Reformar",
    descricao: "Sobrado com boa localização, necessitando de reparos na estrutura elétrica.",
    comodos: 5,
    status: "INATIVO",
    valorAluguel: 1800.00,
    criadoEm: "2026-05-12T16:20:00.000Z",
    aprovado: "NAO",
    avaliado: "SIM",
    metrosQuadrados: "110",
    banheiros: "1",
    tipoDoImovel: "CASA",
    endereco: {
        endereco_id: "end-678901",
        cep: "20020-010",
        uf: "RJ",
        cidade: "Rio de Janeiro",
        bairro: "Lapa",
        rua: "Rua Mem de Sá",
        numero: "85"
    }
};

// 4. Lista de Mocks para uso em testes de listagem/tabelas
export const mockListaImoveis: ImovelResponse[] = [
    mockImovelCompleto,
    mockImovelSemComplemento,
    mockImovelPendente,
    mockImovelComercial,
    mockImovelRural,
];