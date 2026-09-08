import { HttpService } from "@/core/http/httpService";
import type { ImovelResponse } from "@/shared/types/imovel/imovelResponse";
import type { CreateImovelChat } from "../types/createChatImovel";
import { unknown } from "zod";
import { useRouter } from "vue-router";

const router = useRouter();


export class imovelService {
    private readonly httpService = new HttpService();


    async getImoveis(endpoint: string): Promise<ImovelResponse[]> {
        const response = await this.httpService.GetAsync<ImovelResponse[]>(endpoint);

        return response;
    }

    async getImovel(idImovel: string): Promise<ImovelResponse> {
        const response = await this.httpService.GetAsync<ImovelResponse>(
            `imovel/get/buscar/${idImovel}`);

        return response;
    }

    async postChatImovel(request: CreateImovelChat): Promise<{ response: string; success: boolean }> {
        try {
            const response = await this.httpService.PostAsync<string, CreateImovelChat>(
                "Chat/post", request)

            return { response, success: true };
        } catch (error: unknown) {
            console.log(error);
            return { response: `${error}`, success: false }
        }

    }
}