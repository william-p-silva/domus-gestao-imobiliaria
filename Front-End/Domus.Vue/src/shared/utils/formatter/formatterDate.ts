


export const FormatterDate = () => {

    function data(dateUTC:string | Date | null): string{
        if(!dateUTC) return "";

        const date = new Date(dateUTC)

        if(isNaN(date.getTime())) return "";

        return new Intl.DateTimeFormat("pt-BR", {
            day: "2-digit",
            month: "2-digit",
            year: "numeric",
            timeZone: "UTC"
        }).format(date);
    }

    function hora(dateUTC:string | Date | null): string{
        if(!dateUTC) return "";

        const date = new Date(dateUTC)

        if(isNaN(date.getTime())) return "";

        return new Intl.DateTimeFormat("pt-BR", {
            hour: "2-digit",
            minute: "2-digit"
        }).format(date);
    }

    function dataHora(dateUTC:string | Date | null): string{
        if(!dateUTC) return "";

        const date = data(dateUTC);
        const hour = hora(dateUTC);

        if (!data) return "";
        return `${data} às ${hora}`;
    }

    return {
        data,
        hora,
        dataHora
    }
}