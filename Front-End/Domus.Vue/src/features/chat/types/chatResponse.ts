



export interface ResponseChat {
    chat_ID: string,
    imovel_ID: string,
    nome: string,
    estado: string,
    criadoEm: string,
    participantes: ResponseParticipantes[],
    mensagens: ResponseMensagens[]
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


interface ResponseMensagens {
    mensagemChat_ID: string,
    chat_ID: string,
    usuarioChat_ID:string,
    usuario_ID:string,
    texto: string,
    dataEnvio: string    
}