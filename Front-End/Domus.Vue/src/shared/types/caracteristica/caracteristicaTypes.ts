


export enum EstadoCaracteristica {
    Ativa = 0,
    Inativa = 1
}

export type EstadoCaracteristicaType = 'Ativa' | 'Inativa'

export interface GrupoCaracteristicaResponse {
    grupoCaracteristica_ID: string; 
    nome: string;
    codigo: string;
    estado: EstadoCaracteristicaType;
    criadoEm: string;
    deletadoEm?: string | null; 
}

export interface CaracteristicaResponse {
    caracteristica_ID: string; 
    nome: string;
    estado: EstadoCaracteristicaType;
    criadoEm: string;
    deletadoEm?: string | null;
    grupoCaracteristica: GrupoCaracteristicaResponse;
}