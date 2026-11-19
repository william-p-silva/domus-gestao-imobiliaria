<script setup lang="ts">
import type { TableAction, TableColumn, TableFilterOption } from '@/shared/components/table/components/tables';
import TableFilters from '@/shared/components/table/components/tables/components/TableFilters.vue';
import TableSearch from '@/shared/components/table/components/tables/components/TableSearch.vue';
import TableToolbar from '@/shared/components/table/components/tables/components/TableToolbar.vue';
import BaseTable from '@/shared/components/table/components/tables/templates/BaseTable.vue';
import TableBody from '@/shared/components/table/components/tables/templates/TableBody.vue';
import TableFooter from '@/shared/components/table/components/tables/templates/TableFooter.vue';
import TableHeader from '@/shared/components/table/components/tables/templates/TableHeader.vue';
import TableRow from '@/shared/components/table/components/tables/templates/TableRow.vue';
import { useTable } from '@/shared/components/table/composables/useTable';
import type { CaracteristicaResponse } from '@/shared/types/caracteristica/caracteristicaTypes';
import { Eye, History, Pencil } from '@lucide/vue';
import { computed, ref } from 'vue';


interface Props {
    caracteristicas: CaracteristicaResponse[],
    editarAction: (item: CaracteristicaResponse) => void
}

const props = defineProps<Props>();

const activeStatus = ref<string | undefined>();

const items = computed(() => props.caracteristicas);

const filterOptions = computed<TableFilterOption[]>(() => [
    {
        value: 'Todas',
        label: 'Todas',
        count: props.caracteristicas.length
    },
    {
        value: 'Ativa',
        label: 'Ativas',
        count: props.caracteristicas.filter((i) => i.estado === 'Ativa').length
    },
    {
        value: 'Inativa',
        label: 'Inativas',
        count: props.caracteristicas.filter((i) => i.estado === 'Inativa').length
    }
]);

const table = useTable(items, {
    pageSize: 2,
    searchFields: ['nome', 'estado', 'grupoCaracteristica'],
    filterFn: (item) => !activeStatus.value
        || activeStatus.value === 'Todas'
        || item.estado === activeStatus.value
});

const actions: TableAction<CaracteristicaResponse>[] = [
    {
        key: 'edit',
        label: 'Editar característica',
        icon: Pencil,
        onClick: props.editarAction
    },
];

const columns: TableColumn<CaracteristicaResponse>[] = [
    {
        key: 'nome',
        label: 'Característica',
        slot: 'nome', 
        sortable: true

    },
    {
        key: 'grupo',
        label: 'Categoria de exibição',
        slot: 'grupo', 
        sortable: true

    },
    {
        key: 'totalImoveis',
        label: 'Imóveis vinculados',
        align: 'center', 
        sortable: true, 
        format: (value) => `${value ?? 0} imóveis`

    },
    {
        key: 'status',
        label: 'Situação',
        align: 'center',
        slot: 'status'
    }
];

const hasActions = computed(() => actions.length > 0);


function onFilterChange(value: string | number | undefined) {
    activeStatus.value = value === undefined ? undefined : String(value);
    table.setPage(1);
}
</script>



<template>
    <section class="space-y-4">
        <TableToolbar>
            <template #search>
                <div class="max-w-md">
                    <TableSearch v-model="table.search.value" placeholder="Pesquisar característica por nome..." />
                </div>
            </template>

            <template #filters>
                <TableFilters :options="filterOptions" :model-value="activeStatus ?? 'Todas'" 
                @update:model-value="onFilterChange" />
            </template>
        </TableToolbar>

        <BaseTable>
            <TableBody :empty="table.itemsOnPage.value.length === 0" :colspan="columns.length + (hasActions ? 1 : 0)"
                empty-message="Nenhuma característica encontrada." min-width="900px">
                <template #header>
                    <TableHeader :columns="columns" :has-actions="hasActions" :sort-by="table.sortBy.value"
                        :sort-direction="table.sortDirection.value" @sort="table.setSort" />
                </template>


                <TableRow
                    v-for="item in table.itemsOnPage.value"
                    :key="item.caracteristica_ID"
                    :item="item"
                    :columns="columns"
                    :actions="actions"
                >
                    <template #cell-nome="{ item }">
                        <div class="flex min-w-0 items-center gap-3">
                            <div class="flex size-10 shrink-0 items-center justify-center rounded-xl bg-primary-soft text-primary">
                                <span class="text-sm font-semibold">{{ item.nome.trim().slice(0, 1) }}</span>
                            </div>
                            <div class="min-w-0">
                                <p class="truncate font-semibold text-primary-dark">{{ item.nome }}</p>
                                <p class="truncate text-sm text-text-muted">{{ item.grupoCaracteristica.nome }}</p>
                            </div>
                        </div>
                    </template>

                    <template #cell-grupo="{ item }">
                        <span class="inline-flex max-w-full rounded-full bg-blue-50 px-3 py-1.5 text-xs font-medium text-primary-dark">
                            <span class="truncate">{{ item.grupoCaracteristica.nome }}</span>
                        </span>
                    </template>

                    <template #cell-status="{ item }">
                        <span
                            :class="[
                                'inline-flex items-center gap-1.5 whitespace-nowrap rounded-full px-3 py-1.5 text-xs font-medium',
                                item.estado === 'Ativa' ? 'bg-blue-100 text-blue-900' : 'bg-slate-100 text-slate-600'
                            ]"
                        >
                            <span class="size-1.5 rounded-full" :class="item.estado === 'Ativa' ? 'bg-blue-800' : 'bg-slate-400'" />
                            {{ item.estado }} - {{ item.estado === 'Ativa' ? 'Disponível' : 'Oculta' }}
                        </span>
                    </template>
                </TableRow>
            </TableBody>

            <TableFooter
                :showing="table.showing.value"
                :total="table.total.value"
                :page="table.page.value"
                :total-pages="table.totalPages.value"
                item-label="características cadastradas"
                @previous="table.previous"
                @next="table.next"
            />
        </BaseTable>
    </section>
</template>