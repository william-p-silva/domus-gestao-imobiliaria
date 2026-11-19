
<script setup lang="ts">
import { Pencil, Eye, History } from '@lucide/vue';

import {
    BaseTable,
    TableBody,
    TableRow
} from '@/shared/components/table/components/tables';

import type {
    TableAction,
    TableColumn
} from '@/shared/components/table/components/tables';

import { useCaracteristicas } from '../hooks/useCaracteristicas';

import {
    EstadoCaracteristica,
    type CaracteristicaResponse
} from '@/shared/types/caracteristica/caracteristicaTypes';

const {
    caracteristicas,
    filtroEstado,
    carregando,
    erro,
    total,
    alterarFiltro,
    recarregar
} = useCaracteristicas();

// Configuração das colunas
const columns: TableColumn[] = [
    {
        key: 'nome',
        label: 'Característica / Ícone representativo',
        minWidth: '300px',
        slot: 'nome'
    },
    {
        key: 'grupo',
        label: 'Tipo / Categoria de exibição',
        minWidth: '220px',
        slot: 'grupo'
    },
    {
        key: 'total',
        label: 'Imóveis vinculados',
        align: 'center',
        minWidth: '150px'
    },
    {
        key: 'status',
        label: 'Situação / Status',
        align: 'center',
        minWidth: '170px',
        slot: 'status'
    }
];

// Ações disponíveis para cada característica
const getCaracteristicaActions = (
    editar: (item: CaracteristicaResponse) => void,
    alterarEstado: (item: CaracteristicaResponse) => void,
    consultarHistorico: (item: CaracteristicaResponse) => void
): TableAction<CaracteristicaResponse>[] => [
    {
        label: 'Editar característica',
        icon: Pencil,
        onClick: editar
    },
    {
        label: 'Alterar situação',
        icon: Eye,
        onClick: alterarEstado
    },
    {
        label: 'Consultar histórico',
        icon: History,
        onClick: consultarHistorico
    }
];

// Definição dos eventos das ações
const actions = getCaracteristicaActions(
    (item) => {
        console.log('Editar característica:', item);
    },
    (item) => {
        console.log('Alterar situação:', item);
    },
    (item) => {
        console.log('Consultar histórico:', item);
    }
);
</script>

<template>
    <div class="space-y-4">

        <!-- Filtros -->
        <div class="flex flex-wrap gap-2">
            <button
                @click="alterarFiltro('Todas')"
                :class="filtroEstado === 'Todas'
                    ? 'bg-primary text-white'
                    : 'bg-primary-soft text-primary-dark'"
                class="rounded-lg px-4 py-2 text-sm transition-colors"
            >
                Todas
            </button>

            <button
                @click="alterarFiltro(EstadoCaracteristica.Ativa)"
                :class="filtroEstado === EstadoCaracteristica.Ativa
                    ? 'bg-primary text-white'
                    : 'bg-primary-soft text-primary-dark'"
                class="rounded-lg px-4 py-2 text-sm transition-colors"
            >
                Ativas
            </button>

            <button
                @click="alterarFiltro(EstadoCaracteristica.Inativa)"
                :class="filtroEstado === EstadoCaracteristica.Inativa
                    ? 'bg-primary text-white'
                    : 'bg-primary-soft text-primary-dark'"
                class="rounded-lg px-4 py-2 text-sm transition-colors"
            >
                Inativas
            </button>
        </div>

        <!-- Carregamento -->
        <div v-if="carregando">
            Carregando características...
        </div>

        <!-- Erro -->
        <div v-else-if="erro">
            <p>{{ erro }}</p>

            <button @click="recarregar">
                Tentar novamente
            </button>
        </div>

        <!-- Tabela -->
        <BaseTable v-else>
            <TableBody
                :empty="caracteristicas.length === 0"
                empty-message="Nenhuma característica encontrada."
            >
                <TableRow
                    v-for="item in caracteristicas"
                    :key="item.caracteristica_ID"
                    :item="item"
                    :columns="columns"
                    :actions="actions"
                >
                    <!-- Nome -->
                    <template #cell-nome="{ item }">
                        <div>
                            <p class="font-semibold text-primary-dark">
                                {{ item.nome }}
                            </p>
                        </div>
                    </template>

                    <!-- Grupo -->
                    <template #cell-grupo="{ item }">
                        <span class="rounded-full bg-primary-soft px-3 py-1 text-xs text-primary-dark">
                            {{ item.grupoCaracteristica.nome }}
                        </span>
                    </template>

                    <!-- Imóveis vinculados -->
                    <template #cell-total>
                        <span class="text-text-muted">
                            Não informado
                        </span>
                    </template>

                    <!-- Status -->
                    <template #cell-status="{ item }">
                        <span
                            :class="item.estado === 'Ativa'
                                ? 'bg-blue-100 text-blue-800'
                                : 'bg-slate-100 text-slate-600'"
                            class="rounded-full px-3 py-1 text-xs"
                        >
                            {{ item.estado === 'Ativa'
                                ? 'Ativa - Disponível'
                                : 'Inativa - Oculta'
                            }}
                        </span>
                    </template>
                </TableRow>
            </TableBody>
        </BaseTable>

        <!-- Contador -->
        <p class="text-sm text-text-muted">
            Mostrando {{ total }} características
        </p>

    </div>
</template>
