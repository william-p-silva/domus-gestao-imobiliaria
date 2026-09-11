import { string } from "zod";




export interface UserChatsResponse {
    usuarioChat_ID: string,
    usuario_ID: string,
    chat_ID: string,

    email: string, //
    nomeUsuario: string,

    nomeChat: string, //
    funcao: string, //

    estadoMensagem: string, //
    textoMensagem: string, //

    dataUltimaMensagem: string, //

    imagemUrl: string
}