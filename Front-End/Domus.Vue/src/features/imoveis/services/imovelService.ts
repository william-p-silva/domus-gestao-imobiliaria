import { HttpService } from "@/core/http/httpService";
import type { ImovelResponse } from "@/shared/types/imovel/imovelResponse";



export class imovelService {
    private readonly httpService = new HttpService();


    async getImoveis(endpoint: string) : Promise<ImovelResponse[]> {
        const response = await this.httpService.GetAsync<ImovelResponse[]>(endpoint);

        return response;
    }

    async getImovel(idImovel: string) : Promise<ImovelResponse> {
        const response = await this.httpService.GetAsync<ImovelResponse>(
            `imovel/get/buscar/${idImovel}`);

        return response;
    }
}