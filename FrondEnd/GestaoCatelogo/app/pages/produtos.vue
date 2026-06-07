<script setup>
import { ref } from 'vue'
import { useProdutos } from '~/composables/useProdutos'
import { useCategorias } from '~/composables/useCategorias'
import Toast from '~/components/Toast.vue'
import LoadingOverlay from '~/components/LoadingOverlay.vue'

const {
  produtos,
  produtoEditando,
  erro,
  carregando,
  showConfirm,
  showModal,
  carregarProdutos,
  salvarProduto,
  abrirConfirmacao ,
  confirmarExclusao,
  editarProduto,
  novoProduto
} = useProdutos()

const { categorias, carregarCategorias } = useCategorias()

const toastRef = ref(null)
const loadingRef = ref(null)

// Carregar dados ao montar
await carregarProdutos()
await carregarCategorias()

// Exemplo de uso com toast
async function salvar() {
  loadingRef.value?.mostrar()
  try {
    await salvarProduto()
    toastRef.value.addMensagem('Produto salvo com sucesso!', 'success')
    showModal.value = false
  } catch (e) {
    toastRef.value.addMensagem(erro.value, 'error')
  } finally {
    loadingRef.value?.esconder()
  }
}
</script>

<template>
  <div class="container mt-4">
    <h1 class="mb-3">Gerenciamento de Produtos</h1>
    <button class="btn btn-primary mb-3" @click="() => { novoProduto(); showModal = true }">
      Novo Produto
    </button>

    <table class="table table-striped table-bordered">
      <thead class="table-dark">
        <tr>
          <th>Nome</th>
          <th>Descrição</th>
          <th>Preço</th>
          <th>Categoria</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in produtos" :key="p.id">
          <td>{{ p.nome }}</td>
          <td>{{ p.descricao }}</td>
          <td>R$ {{ p.preco }}</td>
          <td>{{ p.categoria?.nome }}</td>
          <td>
            <button class="btn btn-sm btn-warning me-2" @click="() => { editarProduto(p); showModal = true }">
              Editar
            </button>
            <button class="btn btn-sm btn-danger" @click="abrirConfirmacao(p.id)">
              Excluir
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal -->
    <div v-if="showModal" class="modal d-block" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <form @submit.prevent="salvar" novalidate>
            <div class="modal-header">
              <h5 class="modal-title">
                {{ produtoEditando.id ? 'Editar Produto' : 'Novo Produto' }}
              </h5>
              <button type="button" class="btn-close" @click="showModal = false"></button>
            </div>
            <div class="modal-body">
              <!-- Nome -->
              <div class="mb-3">
                <label class="form-label">Nome</label>
                <input
                  v-model="produtoEditando.nome"
                  class="form-control"
                  :class="{ 'is-invalid': produtoEditando.nome.length > 0 && produtoEditando.nome.length < 5 }"
                  placeholder="Nome do produto"
                  required
                />
                <div v-if="produtoEditando.nome.length > 0 && produtoEditando.nome.length < 5" class="invalid-feedback">
                  O campo Nome deve ter pelo menos 5 caracteres.
                </div>
              </div>

              <!-- Descrição -->
              <div class="mb-3">
                <label class="form-label">Descrição</label>
                <input v-model="produtoEditando.descricao" class="form-control" placeholder="Descrição do produto" />
              </div>

              <!-- Preço -->
              <div class="mb-3">
                <label class="form-label">Preço</label>
                <input
                  v-model="produtoEditando.preco"
                  type="number"
                  class="form-control"
                  placeholder="Preço do produto"
                  required
                />
              </div>

              <!-- Categoria -->
              <div class="mb-3">
                <label class="form-label">Categoria</label>
                <select v-model="produtoEditando.categoria" class="form-select" required>
                  <option v-for="c in categorias" :key="c.id" :value="c">
                    {{ c.nome }}
                  </option>
                </select>
                <div class="invalid-feedback">
                  Selecione uma categoria.
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button class="btn btn-success" type="submit" :disabled="produtoEditando.nome.length < 5 || carregando">
                <span v-if="carregando">⏳ Salvando...</span>
                <span v-else>Salvar</span>
              </button>
              <button class="btn btn-secondary" type="button" @click="showModal = false" :disabled="carregando">
                Cancelar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- Modal de confirmação -->
    <div v-if="showConfirm" class="modal d-block" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-danger text-white">
            <h5 class="modal-title">Confirmação de Exclusão</h5>
            <button type="button" class="btn-close" @click="showConfirm = false"></button>
          </div>
          <div class="modal-body">
            <p>Tem certeza que deseja excluir este produto?</p>
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary" @click="showConfirm = false">Cancelar</button>
            <button class="btn btn-danger" @click="confirmarExclusao">Sim, excluir</button>
          </div>
        </div>
      </div>
    </div>

    <Toast ref="toastRef" />
    <LoadingOverlay ref="loadingRef" />
  </div>
</template>

