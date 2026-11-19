export type TableFilterValue = string | number;

export interface TableFilterOption<T extends TableFilterValue = TableFilterValue> {
    value: T;
    label: string;
    count?: number;
}
