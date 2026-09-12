import { HttpService } from "@/core/http/httpService";
import type { ResponseMensagens, ResponseChat } from "../types/chatResponse";
import type { UserChatsResponse } from "../types/userChatsResponse";
import type { EnviarMensagem } from "../types/chatRequest";




export class ChatService {

    private readonly httpService = new HttpService();

    public async getChatByImovelId(imovel_ID: string) : Promise<ResponseChat>{
        const response = await this.httpService.GetAsync<ResponseChat>(
            `chat/get/imovel/${imovel_ID}`)

        return response;
    }

    public async getUserChats() : Promise<UserChatsResponse[]> {
        const response = await this.httpService.GetAsync<UserChatsResponse[]>("chat/get/listar");

        return response;
    }

    public async getChat(chat_id: string) : Promise<ResponseChat>{
        const response = await this.httpService.GetAsync<ResponseChat>(`chat/get/${chat_id}`);

        return response;
    }

    public async enviarMensagemAsync(requestPost: EnviarMensagem){
        const response = await this.httpService.PostAsync<ResponseMensagens, EnviarMensagem>(
            "chat/post/send-message", requestPost
        )

        return response;
    }
}