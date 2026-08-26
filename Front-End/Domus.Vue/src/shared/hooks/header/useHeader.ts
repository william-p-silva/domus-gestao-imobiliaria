import { useAuthStore } from "@/core/configuration/authentication";
import type { LinksHeader, LinksHeaderModal } from "@/shared/types/header/common"
import { ref } from "vue";

const DEFAULT_LINKS: LinksHeader[] = [
    {
        label: "Início",
        route: "/home"
    },
    {
        label: "Imóveis",
        route: "/imoveis"
    },
    {
        label: "Meus Imóveis",
        route: "/imoveis/meus"
    },
    {
        label: "Dúvidas",
        route: "/duvidas"
    }
] 

const DEFAULT_LINKS_PROFILE: LinksHeaderModal[] = [
    {
        label: "Início",
        route: "/home",
        profile: "User"
    },
    {
        label: "Imóveis",
        route: "/imoveis",
        profile: "User"
    },
    {
        label: "Favoritos",
        route: "/favoritos",
        profile: "User"
    },
    {
        label: "Contratos Ativos",
        route: "/contratos",
        profile: "User"
    },
    {
        label: "Conversas",
        route: "/chats",
        profile: "User"
    },
    {
        label: "Meus Imóveis",
        route: "/meus/imoveis",
        profile: "Locador"
    },
    {
        label: "Adicionar Imóvel",
        route: "/imoveis/adicionar",
        profile: "Locador"
    },
    {
        label: "Perfil",
        route: "/perfil",
        profile: "User"
    },
    {
        label: "Configurações",
        route: "/configuracoes",
        profile: "User"
    },
    {
        label: "Dúvidas",
        route: "/duvidas",
        profile: "User"
    }
];

export const useHeader = () => {
    const links: LinksHeader[] = DEFAULT_LINKS;
    const isOpen = ref(false);
    const auth = useAuthStore();
    const allLinks: LinksHeaderModal[] = 
        DEFAULT_LINKS_PROFILE.filter(link => 
            auth.userLogged.perfil.includes(link.profile) || link.profile === 'User');


    function handleNav(){
        isOpen.value = !isOpen.value;
    }

    function open(){
        isOpen.value = true;
    }

    function close(){
        isOpen.value = false;
    }

    return{
        links,
        isOpen,
        allLinks,
        handleNav,
        close,
        open
    }
}