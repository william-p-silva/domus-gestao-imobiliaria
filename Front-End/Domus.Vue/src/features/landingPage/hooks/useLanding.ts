import type { ImovelResponse } from "@/shared/types/imovel/imovelResponse";
import type { InputBuscaImovel, NavLinks, PassoPassoCard, SimpleCardConfig } from "../types/common";
import { mockListaImoveis } from "@/shared/data/mocks/imoveisMock";

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

const DEFAUT_INFOS_INPUTS_BUSCA: InputBuscaImovel[] = [
    {
        text: "Onde você quer morar?",
        placeholder: "Ex: Delmiro Gouveia",
        icon: 'location'
    },
    {
        text: "Tipo do Imóvel",
        placeholder: "Todos os tipos",
        icon: 'arrowDow'
    },
    {
        text: "Faixa de Preço",
        placeholder: "Todas as Faixas",
        icon: 'arrowDow'
    }
]

const DEFAUT_PASSO_PASSO_CARD: PassoPassoCard[] = [
    {
        num: 1,
        title: 'Encontro seu imóvel',
        text: 'Explore imóveis incríveis e use filtros inteligentes para encontrar o que faz sentido para você.',
        img: 'procurando'
    },
    {
        num: 2,
        title: 'Escolha seu lar',
        text: 'Compare opções, veja detalhes, fotos e localização para escolher o imóvel ideal.',
        img: 'encontrar'
    },
    {
        num: 3,
        title: 'Feche com segurança',
        text: 'Negocie com o anunciante, assine o contrato e pronto: seu novo lar te espera.',
        img: 'contratando'
    }
]

const DEFAUT_IMOVEIS_TESTES: ImovelResponse[] = mockListaImoveis;

export const useLanding = () => {
    const links: NavLinks[] = DEFAUT_LINKS;
    const simpleCard: SimpleCardConfig[] = DEFAUT_CONFIG_SIMPLE_CARD;
    const infosBuscaImovel: InputBuscaImovel[] = DEFAUT_INFOS_INPUTS_BUSCA;
    const imoveisFakes: ImovelResponse[] = DEFAUT_IMOVEIS_TESTES;
    const passoAPasso: PassoPassoCard[] = DEFAUT_PASSO_PASSO_CARD;

    return {
        links,
        simpleCard,
        infosBuscaImovel,
        imoveisFakes,
        passoAPasso
    }
} 