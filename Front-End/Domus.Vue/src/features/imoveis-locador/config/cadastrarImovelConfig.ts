import type { ModeIndicator } from "@/shared/components/title/mainTitle.vue"



const DEFAULT_TITLE = {
    title: "Anunciar Imóvel",
    subTitle: "Cadastre seu imóvel e encontre pessoas interessadas em um novo lar com segurança e agilidade institucional.",
    modeIndicator: {
        color: 'accent',
        label: 'Modo de Criação Guiada DOMUS'
    } as ModeIndicator
}

const DEFAULT_TIPOS_IMOVEIS = [
    { label: 'Apartamento', value: 'Apartamento' },
    { label: 'Casa', value: 'Casa' },
    { label: 'Studio', value: 'Studio' },
    { label: 'Kitnet', value: 'KitNet' },
    { label: 'Sobrado', value: 'Sobrado' },
] as const

const DEFAULT_STATUSOPTIONS_IMOVEIS = [
    { label: 'Disponível', value: 'Disponivel' },
    { label: 'Alugado', value: 'Alugado' },
    { label: 'Indisponível', value: 'Indisponivel' },
] as const

export { DEFAULT_TIPOS_IMOVEIS, DEFAULT_STATUSOPTIONS_IMOVEIS, DEFAULT_TITLE }