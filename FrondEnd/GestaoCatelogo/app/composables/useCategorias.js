import { ref, reactive } from 'vue'

export function useCategorias() {
  const categorias = ref([])
  const categoriaEditando = reactive({ id: null, nome: '', descricao: '' })
  const erro = ref(null)
  const carregando = ref(false)
  const { $api } = useNuxtApp()
  const categoriaSelecionada = ref(null)
  const showModal = ref(false)
  const showConfirm = ref(false)

  function abrirConfirmacao(id) {
    categoriaSelecionada.value = id
    showConfirm.value = true
  }

  function confirmarExclusao() {
    showConfirm.value = false
    excluirCategoria(categoriaSelecionada.value) // aqui você chama a função desejada
  }

  // Carregar categorias
  async function carregarCategorias() {  
      const { data: categoriasData } = await $api.get('/categorias')
      categorias.value = categoriasData.data || []  
  }

  // Criar ou atualizar categoria
  async function salvarCategoria() {
    if (categoriaEditando.nome.length < 5) return
    carregando.value = true
    try {
      if (categoriaEditando.id) {
        await $api.put(`/categorias/${categoriaEditando.id}`, categoriaEditando)
        const index = categorias.value.findIndex(c => c.id === categoriaEditando.id)
        categorias.value[index] = { ...categoriaEditando }
      } else {
        const nova = await $api.post('/categorias', categoriaEditando)

        categorias.value.push(nova.data.data)
      }
    } finally {
      carregando.value = false
      showModal.value = false
    }
  }

  // Excluir categoria
  async function excluirCategoria(id) {

    carregando.value = true
    await $api.delete(`/categorias/${id}`)
    categorias.value = categorias.value.filter(c => c.id !== id)

    carregando.value = false
  }

  // Helpers
  function editarCategoria(categoria) {
    Object.assign(categoriaEditando, categoria)
    showModal.value = true
  }

  function novaCategoria() {
    Object.assign(categoriaEditando, { id: null, nome: '', descricao: '' })
    showModal.value = true
  }

  return {
    categorias,
    categoriaEditando,
    erro,
    carregando,
    showModal,
    showConfirm,
    carregarCategorias,
    salvarCategoria,
    editarCategoria,
    novaCategoria,
    abrirConfirmacao,
    confirmarExclusao
  }
}
