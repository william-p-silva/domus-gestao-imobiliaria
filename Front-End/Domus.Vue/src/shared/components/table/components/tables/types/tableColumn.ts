export type TableAlign = 'left' | 'center' | 'right';

/**
 * Configuração de uma coluna.
 * `slot` define o identificador do slot: #cell-{slot}.
 * Quando nenhum slot é fornecido, a tabela apresenta o valor de `key`.
 */
export interface TableColumn<T extends object = Record<string, unknown>> {
    key: string;
    label: string;
    align?: TableAlign;
    width?: string;
    minWidth?: string;
    sortable?: boolean;
    hidden?: boolean;
    slot?: string;
    className?: string;
    headerClassName?: string;
    format?: (value: unknown, item: T) => string | number;
}

export type TableSortDirection = 'asc' | 'desc';
