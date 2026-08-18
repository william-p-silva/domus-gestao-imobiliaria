


export const FormatterNumber = () => {

    function moeda(valor?: number | string | null): string {
        if (valor === undefined || valor === null || valor === "") return "R$ 0,00";

        // Converte para número se for recebido como string
        const numero = typeof valor === "string" ? parseFloat(valor.replace(",", ".")) : valor;

        if (isNaN(numero)) return "R$ 0,00";

        return new Intl.NumberFormat("pt-BR", {
            style: "currency",
            currency: "BRL",
        }).format(numero);
    }

    function metrosQuadrados(
        metros?: number | string | null,
        casasDecimais?: number
    ): string {
        if (metros === undefined || metros === null || metros === "") return "0 m²";

        // Se a string já tiver vírgula vinda do banco/input, substitui por ponto para o parse
        const numero = typeof metros === "string" ? parseFloat(metros.replace(",", ".")) : metros;

        if (isNaN(numero)) return "0 m²";

        const formatador = new Intl.NumberFormat("pt-BR", {
            minimumFractionDigits: casasDecimais ?? 0,
            maximumFractionDigits: casasDecimais ?? 2,
        });

        return `${formatador.format(numero)} m²`;
    }


    return {
        moeda,
        metrosQuadrados
    }
}