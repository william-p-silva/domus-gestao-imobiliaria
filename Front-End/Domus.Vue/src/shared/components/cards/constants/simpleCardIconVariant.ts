export type SimpleCardIconVariant = 
    | 'primary'
    | 'success'
    | 'warning'
    | 'danger'
    | 'info'
    | 'neutral'
    | 'orange'



interface IconStyle {
    background: string;
    color: string;
}

export const SIMPLE_CARD_ICON_STYLES: Record<SimpleCardIconVariant, IconStyle> = {
    ['primary']: {
        background: 'bg-blue-100',
        color: 'text-blue-800'
    },

    ['success']: {
        background: 'bg-green-100',
        color: 'text-green-700'
    },

    ['warning']: {
        background: 'bg-yellow-100',
        color: 'text-yellow-700'
    },

    ['danger']: {
        background: 'bg-red-100',
        color: 'text-red-700'
    },

    ['info']: {
        background: 'bg-sky-100',
        color: 'text-sky-700'
    },

    ['neutral']: {
        background: 'bg-slate-100',
        color: 'text-slate-700'
    },

    ['orange']: {
        background: 'bg-orange-100',
        color: 'text-orange-800'
    }
};