import { HttpService } from "@/core/http/httpService";
import {
    EstadoCaracteristica,
    type CaracteristicaResponse
} from "@/shared/types/caracteristica/caracteristicaTypes";
import type { EstatisticasCaracteristicas } from "../types/caracteristicas";

export class CaracteristicasService {
    private readonly httpService = new HttpService();

    public async GetCaracteristicas(
        estado: EstadoCaracteristica = EstadoCaracteristica.Ativa
    ): Promise<CaracteristicaResponse[]> {
        return this.httpService.GetAsync<CaracteristicaResponse[]>(
            `Caracteristica/get/listar/${estado}`
        );
    }

    public async BuscarEstatisticas(): Promise<EstatisticasCaracteristicas> {
        return this.httpService.GetAsync<EstatisticasCaracteristicas>(
            'Caracteristica/get/buscar/estatisticas');
    }
}