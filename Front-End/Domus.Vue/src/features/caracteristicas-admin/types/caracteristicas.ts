import type { CaracteristicaResponse } from "@/shared/types/caracteristica/caracteristicaTypes";



export interface EstatisticasCaracteristicas {
    ativas: number,
    inativas: number,
    totalImoveis: number,
    porcentagemAtivas: number,
    top: CaracteristicaResponse
}