import { HttpService } from "@/core/http/httpService";
import type { ResponseChat } from "../types/chatResponse";




export class ChatService {

    private readonly httpService = new HttpService();

    async getChatByImovelId(imovel_ID: string) : Promise<ResponseChat>{
        const response = await this.httpService.GetAsync<ResponseChat>(
            `chat/get/${imovel_ID}`)

        return response;
    }
}