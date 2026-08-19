

export interface NavLinks {
    label: string,
    route: string
}


export interface SimpleCardConfig {
    text: string,
    title: string,
    icon: 'ShieldCheck' | 'Heart' | 'UserShield'
}


export interface InputBuscaImovel {
    text: string,
    placeholder: string,
    icon: 'location' | 'arrowDow'
}


export interface PassoPassoCard {
    num: number
    title: string,
    text: string,
    img: 'encontrar' | 'procurando' | 'contratando'
}


export interface BeneficiosCard {
    icon: 'shield' | 'hour' | 'user' | 'support',
    title: string,
    text: string
}