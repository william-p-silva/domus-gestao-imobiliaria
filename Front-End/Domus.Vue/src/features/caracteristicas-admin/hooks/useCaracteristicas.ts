import { ref, computed, onMounted } from 'vue';
import {
    EstadoCaracteristica,
    type CaracteristicaResponse,
} from '@/shared/types/caracteristica/caracteristicaTypes';
import { CaracteristicasService } from '../services/caracteristicas.services';
import type { EstatisticasCaracteristicas } from '../types/caracteristicas';

export type FiltroEstadoCaracteristica =
    | EstadoCaracteristica
    | 'Todas';

export function useCaracteristicas(
    filtroInicial: FiltroEstadoCaracteristica = 'Todas'
) {
    const service = new CaracteristicasService();

    // Dados
    const caracteristicas = ref<CaracteristicaResponse[]>([]);
    const estatisticas = ref<EstatisticasCaracteristicas | null>(null);

    // Controle
    const filtroEstado = ref<FiltroEstadoCaracteristica>(filtroInicial);
    const carregando = ref(false);
    const carregandoEstatisticas = ref(false);

    const erro = ref<string | null>(null);
    const erroEstatisticas = ref<string | null>(null);

    // Controle de requisições concorrentes
    let requisicaoAtual = 0;

    /**
     * Busca as características de acordo com o filtro selecionado.
     */
    async function carregar(
        filtro: FiltroEstadoCaracteristica = filtroEstado.value
    ): Promise<void> {
        filtroEstado.value = filtro;

        const identificador = ++requisicaoAtual;

        carregando.value = true;
        erro.value = null;

        try {
            let resultado: CaracteristicaResponse[];

            if (filtro === 'Todas') {
                const [ativas, inativas] = await Promise.all([
                    service.GetCaracteristicas(EstadoCaracteristica.Ativa),
                    service.GetCaracteristicas(EstadoCaracteristica.Inativa)
                ]);

                resultado = [...ativas, ...inativas];
            } else {
                resultado = await service.GetCaracteristicas(filtro);
            }

            if (identificador !== requisicaoAtual) return;

            caracteristicas.value = resultado;

        } catch (error: unknown) {
            if (identificador !== requisicaoAtual) return;

            erro.value = error instanceof Error
                ? error.message
                : 'Não foi possível carregar as características.';

        } finally {
            if (identificador === requisicaoAtual) {
                carregando.value = false;
            }
        }
    }

    /**
     * Busca as estatísticas das características.
     */
    async function carregarEstatisticas(): Promise<void> {
        carregandoEstatisticas.value = true;
        erroEstatisticas.value = null;

        try {
            estatisticas.value = await service.BuscarEstatisticas();

        } catch (error: unknown) {
            erroEstatisticas.value = error instanceof Error
                ? error.message
                : 'Não foi possível carregar as estatísticas.';

        } finally {
            carregandoEstatisticas.value = false;
        }
    }

    /**
     * Atualiza o filtro e busca novamente.
     */
    function alterarFiltro(filtro: FiltroEstadoCaracteristica) {
        return carregar(filtro);
    }

    /**
     * Recarrega a listagem e as estatísticas.
     */
    async function recarregar(): Promise<void> {
        await Promise.all([
            carregar(),
            carregarEstatisticas()
        ]);
    }

    // Informações auxiliares
    const total = computed(() => caracteristicas.value.length);

    // Carregamento inicial
    onMounted(() => {
        void Promise.all([
            carregar(),
            carregarEstatisticas()
        ]);
    });

    return {
        caracteristicas,
        estatisticas,

        filtroEstado,
        carregando,
        carregandoEstatisticas,

        erro,
        erroEstatisticas,

        total,

        carregar,
        carregarEstatisticas,
        alterarFiltro,
        recarregar
    };
}