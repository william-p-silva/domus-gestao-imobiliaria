import type { Component } from 'vue';

export type TableActionVariant = 'default' | 'primary' | 'danger';

export interface TableAction<T extends object = Record<string, unknown>> {
    /** Identificador estável e único dentro das ações da linha. */
    key: string;
    /** Texto acessível e tooltip do botão. */
    label: string;
    /** Ícone opcional (Lucide ou qualquer componente Vue). */
    icon?: Component;
    onClick: (item: T) => void;
    visible?: (item: T) => boolean;
    disabled?: (item: T) => boolean;
    variant?: TableActionVariant;
}
