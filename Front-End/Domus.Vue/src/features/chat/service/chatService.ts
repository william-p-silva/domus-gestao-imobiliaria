import { HttpService } from "@/core/http/httpService";
import type { ResponseChat } from "../types/chatResponse";
import type { UserChatsResponse } from "../types/userChatsResponse";




export class ChatService {

    private readonly httpService = new HttpService();

    public async getChatByImovelId(imovel_ID: string) : Promise<ResponseChat>{
        const response = await this.httpService.GetAsync<ResponseChat>(
            `chat/get/imovel/${imovel_ID}`)

        return response;
    }

    public async getUserChats(){
        const response = await this.httpService.GetAsync<UserChatsResponse[]>("chat/get/listar");

        return response;
    }
}