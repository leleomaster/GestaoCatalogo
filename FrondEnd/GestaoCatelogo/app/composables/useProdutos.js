import { ref, reactive } from 'vue'

export function useProdutos() {
    // Estado reativo
    const produtos = ref([])
    const produtoEditando = reactive({ id: null, nome: '', descricao: '', preco: null, categoria: { id: null, nome: '' } })
    const showModal = ref(false)
    const erro = ref(null)
    const carregando = ref(false)
    const { $api } = useNuxtApp()
    const showConfirm = ref(false)

    function abrirConfirmacao(id) {
        produtoEditando.value = id
        showConfirm.value = true
    }

    function confirmarExclusao() {
        showConfirm.value = false
        excluirProduto(produtoEditando.value) // aqui você chama a função desejada
    }

    // Criar ou atualizar produto
    async function salvarProduto() {
        carregando.value = true
        if (produtoEditando.nome.length < 5) return
        try {
            if (produtoEditando.id) {
                await $api.put(`/produtos/${produtoEditando.id}`, produtoEditando)
                const index = produtos.value.findIndex(p => p.id === produtoEditando.id)
                produtos.value[index] = JSON.parse(JSON.stringify(produtoEditando))
            } else {
                const novo = await $api.post('/produtos', produtoEditando)
                let novaProduto = JSON.parse(JSON.stringify(novo)).data.data
                produtos.value.push(novaProduto)
            }
            showModal.value = false
        } finally {
            carregando.value = false
        }
    }

    // Excluir produto
    async function excluirProduto(id) {
        carregando.value = true

        await $api.delete(`/produtos/${id}`)
        produtos.value = produtos.value.filter(p => p.id !== id)

        carregando.value = false
    }

    // Abrir modal de edição
    function editarProduto(produto) {
        Object.assign(produtoEditando, JSON.parse(JSON.stringify(produto)))
        showModal.value = true
    }

    // Novo produto
    function novoProduto() {
        Object.assign(produtoEditando, { id: null, nome: '', descricao: '', preco: null, categoria: { id: null, nome: '' } })
        showModal.value = true
    }

    // Carregar dados iniciais
    async function carregarProdutos() {
        const { data: produtoData } = await $api.get('/produtos')
        produtos.value = produtoData.data || []
    }

    return {
        produtos,
        produtoEditando,
        erro,
        carregando,
        carregarProdutos,
        salvarProduto,
        excluirProduto,
        editarProduto,
        novoProduto,
        abrirConfirmacao,
        confirmarExclusao,
        showConfirm
    }
}