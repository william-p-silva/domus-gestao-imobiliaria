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
        profile: 'Admin'
    },
    {
        label: "Imóveis",
        route: "/imoveis",
        profile: 'Locador'

    },
    {
        label: "Meus Imóveis",
        route: "/imoveis/meus",
        profile: 'Locador'
    },
    {
        label: "Dúvidas",
        route: "/duvidas",
        profile: 'Locatario'
    }
] 


export const useHeader = () => {
    const links: LinksHeader[] = DEFAULT_LINKS;
    const isOpen = ref(false);

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
        handleNav,
        close,
        open
    }
}