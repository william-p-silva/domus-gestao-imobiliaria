<script setup lang="ts">
import { computed, ref } from 'vue';
import { Eye, Pencil, History } from '@lucide/vue';
import { useTable } from '@/shared/components/table/composables/useTable';
import {
    BaseTable,
    TableBody,
    TableFooter,
    TableHeader,
    TableRow,
    TableSearch,
    TableToolbar,
    TableFilters,
    type TableAction,
    type TableColumn,
    type TableFilterOption,
} from '@/shared/components/table/components/tables';

interface Caracteristica {
    id: number;
    nome: string;
    descricao: string;
    grupo: string;
    totalImoveis: number;
    status: 'Ativa' | 'Inativa';
}

const caracteristicas = ref<Caracteristica[]>([
    { id: 1, nome: 'Piscina', descricao: 'Área molhada e lazer coletivo', grupo: 'Infraestrutura do Condomínio', totalImoveis: 28, status: 'Ativa' },
    { id: 2, nome: 'Ar-condicionado', descricao: 'Aparelho Split ou Central', grupo: 'Comodidade Privativa', totalImoveis: 42, status: 'Ativa' },
    { id: 3, nome: 'Academia', descricao: 'Espaço fitness equipado', grupo: 'Infraestrutura do Condomínio', totalImoveis: 35, status: 'Ativa' },
    { id: 4, nome: 'Varanda Gourmet', descricao: 'Sacada com ponto grill', grupo: 'Comodidade Privativa', totalImoveis: 31, status: 'Ativa' },
    { id: 5, nome: 'Sauna úmida', descricao: 'Banho a vapor condominial', grupo: 'Infraestrutura de Lazer', totalImoveis: 4, status: 'Inativa' },
    { id: 6, nome: 'Carregador para carro elétrico', descricao: 'Tomada dedicada tipo Wallbox', grupo: 'Sustentabilidade & Vagas', totalImoveis: 2, status: 'Inativa' }
]);

const activeStatus = ref<string | undefined>();
const filterOptions: TableFilterOption[] = [
    { 
        value: 'Todas', 
        label: 'Todas', 
        count: caracteristicas.value.length 
    },
    { value: 'Ativa', label: 'Ativas', count: caracteristicas.value.filter((item) => item.status === 'Ativa').length },
    { value: 'Inativa', label: 'Inativas', count: caracteristicas.value.filter((item) => item.status === 'Inativa').length }
];

const table = useTable(caracteristicas, {
    pageSize: 4,
    searchFields: ['nome', 'descricao', 'grupo'],
    filterFn: (item) => !activeStatus.value || activeStatus.value === 'Todas' || item.status === activeStatus.value
});

const columns: TableColumn<Caracteristica>[] = [
    { key: 'nome', label: 'Característica / Ícone representativo', minWidth: '290px', slot: 'nome', sortable: true },
    { key: 'grupo', label: 'Tipo / Categoria de exibição', minWidth: '220px', slot: 'grupo', sortable: true },
    { key: 'totalImoveis', label: 'Imóveis vinculados', align: 'center', minWidth: '150px', sortable: true, format: (value) => `${value ?? 0} imóveis` },
    { key: 'status', label: 'Situação / Status', align: 'center', minWidth: '170px', slot: 'status' }
];

const actions: TableAction<Caracteristica>[] = [
    { key: 'edit', label: 'Editar característica', icon: Pencil, onClick: (item) => console.log('Editar', item) },
    { key: 'view', label: 'Visualizar característica', icon: Eye, onClick: (item) => console.log('Visualizar', item) },
    { key: 'history', label: 'Consultar histórico', icon: History, onClick: (item) => console.log('Histórico', item) }
];

const hasActions = computed(() => actions.length > 0);

function onFilterChange(value: string | number | undefined) {
    activeStatus.value = value === undefined ? undefined : String(value);
    table.setPage(1);
}
</script>

<template>
    <div class="space-y-4">
        <TableToolbar>
            <template #search>
                <div class="max-w-md">
                    <TableSearch v-model="table.search.value" placeholder="Pesquisar característica por nome..." />
                </div>
            </template>

            <template #filters>
                <TableFilters
                    :options="filterOptions"
                    :model-value="activeStatus ?? 'Todas'"
                    @update:model-value="onFilterChange"
                />
            </template>
        </TableToolbar>

        <BaseTable>
            <TableBody
                :empty="table.itemsOnPage.value.length === 0"
                :colspan="columns.length + (hasActions ? 1 : 0)"
                empty-message="Nenhuma característica encontrada."
                min-width="900px"
            >
                <template #header>
                    <TableHeader
                        :columns="columns"
                        :has-actions="hasActions"
                        :sort-by="table.sortBy.value"
                        :sort-direction="table.sortDirection.value"
                        @sort="table.setSort"
                    />
                </template>

                <TableRow
                    v-for="item in table.itemsOnPage.value"
                    :key="item.id"
                    :item="item"
                    :columns="columns"
                    :actions="actions"
                >
                    <template #cell-nome="{ item }">
                        <div class="flex min-w-0 items-center gap-3">
                            <div class="flex size-10 shrink-0 items-center justify-center rounded-xl bg-primary-soft text-primary">
                                <span class="text-sm font-semibold">{{ item.nome.slice(0, 1) }}</span>
                            </div>
                            <div class="min-w-0">
                                <p class="truncate font-semibold text-primary-dark">{{ item.nome }}</p>
                                <p class="truncate text-sm text-text-muted">{{ item.descricao }}</p>
                            </div>
                        </div>
                    </template>

                    <template #cell-grupo="{ item }">
                        <span class="inline-flex max-w-full rounded-full bg-blue-50 px-3 py-1.5 text-xs font-medium text-primary-dark">
                            <span class="truncate">{{ item.grupo }}</span>
                        </span>
                    </template>

                    <template #cell-status="{ item }">
                        <span
                            :class="[
                                'inline-flex items-center gap-1.5 whitespace-nowrap rounded-full px-3 py-1.5 text-xs font-medium',
                                item.status === 'Ativa' ? 'bg-blue-100 text-blue-900' : 'bg-slate-100 text-slate-600'
                            ]"
                        >
                            <span class="size-1.5 rounded-full" :class="item.status === 'Ativa' ? 'bg-blue-800' : 'bg-slate-400'" />
                            {{ item.status }} - {{ item.status === 'Ativa' ? 'Disponível' : 'Oculta' }}
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
    </div>
</template>
