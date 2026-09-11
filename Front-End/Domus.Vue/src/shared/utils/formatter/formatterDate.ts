


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

    function dayMoth(dateUTC:string | Date | null): string{
        if(!dateUTC) return "";

        const date = new Date(dateUTC)

        if(isNaN(date.getTime())) return "";

        return new Intl.DateTimeFormat("pt-BR", {
            day: "2-digit",
            month: "2-digit",
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

    function dataHora(dateUTC: string | Date | null): string {
        if (!dateUTC) return "";

        const formattedDate = data(dateUTC);
        const formattedHour = hora(dateUTC);

        if (!formattedDate || !formattedHour) return "";
        
        return `${formattedDate} às ${formattedHour}`;
    }

    return {
        data,
        hora,
        dayMoth,
        dataHora
    }
}