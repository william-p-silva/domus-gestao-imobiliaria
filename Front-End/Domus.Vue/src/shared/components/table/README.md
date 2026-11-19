# Biblioteca de tabelas — DOMUS

Implementação de componentes reutilizáveis para Vue 3 + TypeScript + Tailwind CSS.

## Estrutura

- `BaseTable`: superfície visual, borda, fundo e arredondamento.
- `TableBody`: `<table>`, rolagem horizontal, cabeçalho via slot e estados de carregamento/vazio.
- `TableHeader`: colunas configuráveis e evento de ordenação.
- `TableRow`: renderização genérica de uma linha, valores padrão e slots `cell-{nome}`.
- `TableCell`: alinhamento e dimensões de uma célula.
- `TableActions`: ações configuráveis por registro.
- `TableFooter`: contador e navegação de páginas.
- `TableToolbar`: composição de pesquisa, filtros e ações gerais.
- `TableSearch` e `TableFilters`: controles visuais opcionais.
- `useTable`: pesquisa, filtro externo, ordenação e paginação em memória.

## Instalação no projeto

Copie o conteúdo de `src/shared` para a pasta `src/shared` do DOMUS. Os componentes não dependem de um pacote de ícones específico: passe qualquer componente Vue na propriedade `icon` de uma ação.

A demonstração em `examples/CaracteristicasTableExample.vue` usa `lucide-vue-next` apenas para ilustrar as ações. Ajuste o import dos ícones para o pacote já instalado no projeto ou substitua pelos ícones que você utiliza.

## Exemplo de composição

```vue
<script setup lang="ts">
import { ref } from 'vue';
import {
  BaseTable, TableBody, TableFooter, TableHeader, TableRow,
  TableToolbar, TableSearch, TableFilters,
  type TableColumn, type TableAction
} from '@/shared/components/tables';
import { useTable } from '@/shared/composables/useTable';

interface Usuario {
  id: number;
  nome: string;
  email: string;
  status: string;
}

const usuarios = ref<Usuario[]>([]);
const columns: TableColumn<Usuario>[] = [
  { key: 'nome', label: 'Usuário', sortable: true, slot: 'usuario' },
  { key: 'email', label: 'E-mail', sortable: true },
  { key: 'status', label: 'Status', slot: 'status' }
];
const actions: TableAction<Usuario>[] = [];
const table = useTable(usuarios, {
  pageSize: 10,
  searchFields: ['nome', 'email']
});
</script>

<template>
  <TableToolbar>
    <template #search>
      <TableSearch v-model="table.search.value" placeholder="Pesquisar usuário..." />
    </template>
    <template #filters>
      <!-- Coloque TableFilters ou filtros específicos da tela aqui. -->
    </template>
  </TableToolbar>

  <BaseTable>
    <TableBody
      :empty="table.itemsOnPage.value.length === 0"
      :colspan="columns.length + (actions.length ? 1 : 0)"
    >
      <template #header>
        <TableHeader
          :columns="columns"
          :has-actions="actions.length > 0"
          :sort-by="table.sortBy.value"
          :sort-direction="table.sortDirection.value"
          @sort="table.setSort"
        />
      </template>

      <TableRow
        v-for="usuario in table.itemsOnPage.value"
        :key="usuario.id"
        :item="usuario"
        :columns="columns"
        :actions="actions"
      >
        <template #cell-usuario="{ item }">
          <div>
            <p class="font-semibold">{{ item.nome }}</p>
            <p class="text-sm text-slate-500">{{ item.email }}</p>
          </div>
        </template>
      </TableRow>
    </TableBody>

    <TableFooter
      :showing="table.showing.value"
      :total="table.total.value"
      :page="table.page.value"
      :total-pages="table.totalPages.value"
      item-label="usuários"
      @previous="table.previous"
      @next="table.next"
    />
  </BaseTable>
</template>
```

## Contratos principais

### Colunas

`TableColumn<T>` aceita:

- `key`: propriedade do registro usada como valor padrão e identificador da coluna.
- `label`: texto do cabeçalho.
- `align`: `left`, `center` ou `right`.
- `width` e `minWidth`: dimensões CSS (ex.: `180px`, `20%`).
- `sortable`: habilita o botão que emite `sort` com a chave da coluna.
- `hidden`: oculta a coluna no cabeçalho e nas linhas.
- `slot`: nome alternativo do slot `cell-{slot}`.
- `format`: transforma o valor padrão apresentado na célula.
- `className` e `headerClassName`: classes adicionais.

### Slots de célula

O nome padrão é `cell-{column.key}`. O slot recebe `{ item, value, column }`. Exemplo:

```vue
<template #cell-status="{ item, value, column }">
  <!-- composição livre da tela -->
</template>
```

Sem slot, o componente mostra o valor da propriedade ou o retorno de `format`.

### Ações

`TableAction<T>` aceita `key`, `label`, `icon`, `onClick(item)`, `visible(item)`, `disabled(item)` e `variant` (`default`, `primary`, `danger`). `icon` é opcional; quando não informado, o botão apresenta o texto da ação.

### useTable

```ts
const table = useTable(registros, {
  pageSize: 10,
  searchFields: ['nome', 'email'],
  filterFn: (item) => item.ativo === somenteAtivos.value
});
```

Retorna `search`, `page`, `pageSize`, `sortBy`, `sortDirection`, `filteredItems`, `sortedItems`, `itemsOnPage`, `showing`, `total`, `totalPages`, `setSort`, `setPage`, `previous`, `next` e `clearSearch`.

`filterFn` pode consultar refs da tela para filtros reativos. O composable trabalha em memória; para paginação e filtros feitos pela API, controle os dados e a página no componente de tela e utilize apenas os componentes visuais.

## Observações

- A ordenação do `useTable` é local e compara números, datas e textos.
- O evento `sort` do `TableHeader` não altera dados por conta própria. Conecte-o a `table.setSort` ou à ordenação server-side.
- O estado vazio/carregando do `TableBody` usa `colspan`; informe a quantidade de colunas visíveis mais uma coluna de ações, quando houver.
- Para tabelas com menos colunas, ajuste `min-width` do `TableBody` conforme necessário.
- A biblioteca usa classes Tailwind estáticas para que o scanner consiga identificá-las.
