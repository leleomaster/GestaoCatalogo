<script setup>
import { ref, computed } from 'vue'
import { useProdutos } from '~/composables/useProdutos'
import { useCategorias } from '~/composables/useCategorias'
import LoadingOverlay from '~/components/LoadingOverlay.vue'
import { Pie, Bar } from 'vue-chartjs'
import {
    Chart as ChartJS,
    Title,
    Tooltip,
    Legend,
    ArcElement,
    BarElement,
    CategoryScale,
    LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement, BarElement, CategoryScale, LinearScale)

const { produtos, carregarProdutos } = useProdutos()
const { categorias, carregarCategorias } = useCategorias()

const loadingRef = ref(null)
const filtroPeriodo = ref('30') // padrão: últimos 30 dias

onMounted(() => {

    // Carregar dados iniciais
    loadingRef.value?.mostrar()
    carregarProdutos()
    carregarCategorias()

    loadingRef.value?.esconder()
})

// Função para filtrar produtos por período
function filtrarProdutosPorPeriodo() {
    
    const dias = parseInt(filtroPeriodo.value)
    const limite = new Date()
    limite.setDate(limite.getDate() - dias)
    return produtos.value.filter(p => p !== null)//filter(p => new Date(p.criadoEm) >= limite)
}

// Dados para gráfico de pizza (produtos por categoria)
const produtosPorCategoria = computed(() => {
    const filtrados = filtrarProdutosPorPeriodo()
    return categorias.value.map(c => {
        return filtrados.filter(p => p.id === c.id).length
    })
})

const pieData = computed(() => ({
    labels: categorias.value.map(c => c.nome),
    datasets: [
        {
            data: produtosPorCategoria.value,
            backgroundColor: ['#3498db', '#2ecc71', '#e74c3c', '#9b59b6', '#f1c40f']
        }
    ]
}))

// Dados para gráfico de barras (resumo geral)
const barData = computed(() => {
    const filtrados = filtrarProdutosPorPeriodo()
    return {
        labels: ['Produtos (filtrados)', 'Categorias'],
        datasets: [
            {
                label: 'Quantidade',
                data: [filtrados.length, categorias.value.length],
                backgroundColor: ['#2980b9', '#27ae60']
            }
        ]
    }
})
</script>

<template>
    <div>
        <h1>Dashboard do Estoque</h1>

        <div class="cards">
            <div class="card">
                <h2>Produtos</h2>
                <p>{{ filtrarProdutosPorPeriodo().length }}</p>
            </div>
            <div class="card">
                <h2>Categorias</h2>
                <p>{{ categorias.length }}</p>
            </div>
        </div>

        <div class="charts">
            <div class="chart">
                <h3>Distribuição de Produtos por Categoria</h3>
                <Pie :data="pieData" />
            </div>
            <div class="chart">
                <h3>Resumo Geral</h3>
                <Bar :data="barData" />
            </div>
        </div>

        <LoadingOverlay ref="loadingRef" />
    </div>
</template>

<style scoped>
.filtro {
    margin-top: 1rem;
}

.cards {
    display: flex;
    gap: 2rem;
    margin-top: 2rem;
}

.card {
    flex: 1;
    background: #fff;
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 1.5rem;
    text-align: center;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
}

.charts {
    display: flex;
    gap: 2rem;
    margin-top: 3rem;
}

.chart {
    flex: 1;
    background: #fff;
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 1rem;
}
</style>
