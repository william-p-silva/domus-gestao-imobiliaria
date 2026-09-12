


export const FormatterDate = () => {

    function parseUtc(dateUTC: string | Date | null): Date | null {
        if (!dateUTC) return null;

        let date: Date;

        if (dateUTC instanceof Date) {
            date = dateUTC;
        } else {
            // trunca frações de segundo para 3 dígitos (padrão JS)
            // e adiciona 'Z' se não houver indicador de timezone,
            // já que o backend manda a data em UTC sem marcá-la como tal
            const normalized = dateUTC
                .replace(/(\.\d{3})\d*$/, "$1") // corta milissegundos extras
                .replace(/(?<!Z)$/, (match, offset, str) =>
                    /[Z+-]\d{2}:?\d{2}$|Z$/.test(str) ? "" : "Z"
                );

            date = new Date(normalized);
        }

        return isNaN(date.getTime()) ? null : date;
    }

    function data(dateUTC: string | Date | null): string {
        const date = parseUtc(dateUTC);
        if (!date) return "";

        return new Intl.DateTimeFormat("pt-BR", {
            day: "2-digit",
            month: "2-digit",
            year: "numeric",
        }).format(date);
    }

    function dayMoth(dateUTC: string | Date | null): string {
        const date = parseUtc(dateUTC);
        if (!date) return "";

        return new Intl.DateTimeFormat("pt-BR", {
            day: "2-digit",
            month: "2-digit",
        }).format(date);
    }

    function hora(dateUTC: string | Date | null): string {
        const date = parseUtc(dateUTC);
        if (!date) return "";

        return new Intl.DateTimeFormat("pt-BR", {
            hour: "2-digit",
            minute: "2-digit"
        }).format(date);
    }

    function dataHora(dateUTC: string | Date | null): string {
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


function parseUtc(dateUTC: string | Date | null): Date | null {
    if (!dateUTC) return null;

    let date: Date;

    if (dateUTC instanceof Date) {
        date = dateUTC;
    } else {
        let str = dateUTC.trim();

        // corta milissegundos para 3 dígitos, se houver mais
        str = str.replace(/\.(\d{3})\d+/, ".$1");

        // se não termina com Z e não tem offset (+HH:MM ou -HH:MM), assume UTC
        const hasTimezone = /Z$|[+-]\d{2}:\d{2}$/.test(str);
        if (!hasTimezone) {
            str += "Z";
        }

        date = new Date(str);
    }

    return isNaN(date.getTime()) ? null : date;
}