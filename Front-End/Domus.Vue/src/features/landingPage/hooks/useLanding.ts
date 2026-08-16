import type { NavLinks } from "../types/common";

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

export const useLanding = () => {
    const links: NavLinks[] = DEFAUT_LINKS;


    return {
        links
    }
} 