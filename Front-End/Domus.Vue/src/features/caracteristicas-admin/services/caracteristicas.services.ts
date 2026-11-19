import { HttpService } from "@/core/http/httpService";
import {
    EstadoCaracteristica,
    type CaracteristicaResponse
} from "@/shared/types/caracteristica/caracteristicaTypes";

export class CaracteristicasService {
    private readonly httpService = new HttpService();

    public async GetCaracteristicas(
        estado: EstadoCaracteristica = EstadoCaracteristica.Ativa
    ): Promise<CaracteristicaResponse[]> {
        return this.httpService.GetAsync<CaracteristicaResponse[]>(
            `Caracteristica/get/listar/${estado}`
        );
    }
}