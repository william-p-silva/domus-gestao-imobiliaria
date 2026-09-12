
// Adicionar Imovel response junto :)


export interface ResponseChat {
    chat_ID: string,
    nome: string,
    estado: string,
    criadoEm: string,
    participantes: ResponseParticipantes[],
    mensagens: ResponseMensagens[]
    imovel: ResponseImovelChat
}


interface ResponseParticipantes {
    usuarioChat_ID: string,
    usuario_ID: string,
    nome: string,
    email: string,
    funcao: string,
    estado: string,
    criadoEm: string
}


export interface ResponseMensagens {
    mensagemChat_ID: string,
    usuario_ID: string,
    chat_ID: string,
    estado: string,
    texto: string,
    dataEnvio: string    
}

export interface ResponseImovelChat {
    imovel_ID: string,
    titulo: string,
    Descricao: string,
    comodos: string,
    banheiros: string,
    metrosQuadrados: string,
    valorAluguel:string,
    imagens: ResponseImagensImovel[]
}

export interface ResponseImagensImovel {
    imagemImovel_ID: string
    UrlImagem: string
    Titulo: string
}
