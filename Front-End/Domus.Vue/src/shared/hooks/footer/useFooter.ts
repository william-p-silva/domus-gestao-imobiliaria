import type { LinksFooter } from "@/shared/types/footer/common"

const DEFAUT_LINKS_FOOTER: LinksFooter[] = [
    {
        title: "Navegação",
        links: [
            {
                route: "/",
                label: "Inicio"
            },
            {
                route: "/imoveis",
                label: "Imóveis"
            },
            {
                route: "/#como-funciona",
                label: "Como Funciona"
            },
            {
                route: "/duvidas",
                label: "Dúvidas"
            }
        ]
    },
    {
        title: "Institucional",
        links: [
            {
                route: "/sobre",
                label: "Sobre nós"
            },
            {
                route: "/blog",
                label: "Blog"
            },
            {
                route: "/emprego",
                label: "Trabalhe Conosco"
            },
            {
                route: "/contato",
                label: "Contato"
            }
        ]
    },
    {
        title: "Suporte",
        links: [
            {
                route: "/central/ajuda",
                label: "Central de Ajuda"
            },
            {
                route: "/politica/privacidade",
                label: "Política de Privacidade"
            },
            {
                route: "/central/termos-uso",
                label: "Termos de Uso"
            },
            {
                route: "/central/seguranca",
                label: "Segurança"
            }
        ]
    }

]


export const useFooter = () => {
    const links: LinksFooter[] = DEFAUT_LINKS_FOOTER


    return {
        links
    }
}