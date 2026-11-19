import { computed, ref, toValue, watch, type MaybeRefOrGetter } from 'vue';
import type { TableSortDirection } from '../components/tables/types/tableColumn';

export interface UseTableOptions<T extends object> {
    pageSize?: number;
    /** Campos pesquisáveis quando `searchFn` não for informado. */
    searchFields?: (keyof T)[];
    /** Permite definir uma estratégia própria de pesquisa. Recebe o termo já normalizado. */
    searchFn?: (item: T, normalizedQuery: string) => boolean;
    /** Filtro externo opcional. Pode consultar refs para acompanhar filtros reativos da tela. */
    filterFn?: (item: T) => boolean;
}

function compareValues(left: unknown, right: unknown): number {
    if (left == null && right == null) return 0;
    if (left == null) return -1;
    if (right == null) return 1;

    if (typeof left === 'number' && typeof right === 'number') {
        return left - right;
    }

    if (left instanceof Date && right instanceof Date) {
        return left.getTime() - right.getTime();
    }

    return String(left).localeCompare(String(right), 'pt-BR', {
        numeric: true,
        sensitivity: 'base'
    });
}

/** Pesquisa, filtro externo, ordenação e paginação em memória. */
export function useTable<T extends object>(
    source: MaybeRefOrGetter<readonly T[]>,
    options: UseTableOptions<T> = {}
) {
    const search = ref('');
    const page = ref(1);
    const pageSize = ref(Math.max(1, options.pageSize ?? 10));
    const sortBy = ref<string>();
    const sortDirection = ref<TableSortDirection>('asc');

    const sourceItems = computed(() => toValue(source));

    const filteredItems = computed(() => {
        const query = search.value.trim().toLocaleLowerCase('pt-BR');

        return sourceItems.value.filter((item) => {
            if (options.filterFn && !options.filterFn(item)) return false;
            if (!query) return true;

            if (options.searchFn) return options.searchFn(item, query);

            const fields = options.searchFields ?? [];
            return fields.some((field) => {
                const value = item[field];
                return value !== null
                    && value !== undefined
                    && String(value).toLocaleLowerCase('pt-BR').includes(query);
            });
        });
    });

    const sortedItems = computed(() => {
        if (!sortBy.value) return filteredItems.value;

        const key = sortBy.value;
        const direction = sortDirection.value === 'asc' ? 1 : -1;

        return [...filteredItems.value].sort((a, b) => {
            const left = (a as Record<string, unknown>)[key];
            const right = (b as Record<string, unknown>)[key];
            return compareValues(left, right) * direction;
        });
    });

    const total = computed(() => sortedItems.value.length);
    const totalPages = computed(() => Math.max(1, Math.ceil(total.value / pageSize.value)));

    const itemsOnPage = computed(() => {
        const start = (page.value - 1) * pageSize.value;
        return sortedItems.value.slice(start, start + pageSize.value);
    });

    const showing = computed(() => itemsOnPage.value.length);

    watch(search, () => {
        page.value = 1;
    });

    watch(totalPages, (lastPage) => {
        if (page.value > lastPage) page.value = lastPage;
    });

    watch(pageSize, (size) => {
        pageSize.value = Math.max(1, size);
        page.value = 1;
    });

    function setSort(key: string) {
        if (sortBy.value === key) {
            sortDirection.value = sortDirection.value === 'asc' ? 'desc' : 'asc';
        } else {
            sortBy.value = key;
            sortDirection.value = 'asc';
        }
        page.value = 1;
    }

    function setPage(nextPage: number) {
        page.value = Math.min(Math.max(1, nextPage), totalPages.value);
    }

    function previous() {
        setPage(page.value - 1);
    }

    function next() {
        setPage(page.value + 1);
    }

    function clearSearch() {
        search.value = '';
    }

    return {
        search,
        page,
        pageSize,
        sortBy,
        sortDirection,
        filteredItems,
        sortedItems,
        itemsOnPage,
        showing,
        total,
        totalPages,
        setSort,
        setPage,
        previous,
        next,
        clearSearch
    };
}
