import type { NavLinks, SimpleCardConfig } from "../types/common";

const DEFAUT_LINKS: NavLinks[] = [
    {
        label: "Inicio",
        route: "/",            
    },
    {
        label: "Imóveis",
        route: "/imoveis"
    },
    {
        label: "Como Funciona",
        route: "#como-funciona",
    },
    {
        label: "Sobre Nós",
        route: "#sobre-nos",
    },
    {
        label: "Dúvidas",
        route: "#duvidas",
    }
]

const DEFAUT_CONFIG_SIMPLE_CARD: SimpleCardConfig[] = [
    {
        title: "Seguro",
        text: "Ambiente Verificado",
        icon: 'ShieldCheck'
    },
    {
        title: "Confiável",
        text: "Anúncios reais e atualizados",
        icon: 'UserShield'
    },
    {
        title: "Feito para você",
        text: "Experiéncia personalizada",
        icon: 'Heart'
    }
]

export const useLanding = () => {
    const links: NavLinks[] = DEFAUT_LINKS;
    const simpleCard: SimpleCardConfig[] = DEFAUT_CONFIG_SIMPLE_CARD;

    return {
        links,
        simpleCard
    }
} 