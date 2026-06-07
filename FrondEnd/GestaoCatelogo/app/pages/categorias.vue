<script setup>
import { ref } from 'vue'
import { useCategorias } from '~/composables/useCategorias'
import Toast from '~/components/Toast.vue'
import LoadingOverlay from '~/components/LoadingOverlay.vue'

const {
  categorias,
  categoriaEditando,
  erro,
  carregando,
  carregarCategorias,
  salvarCategoria,
  showModal,
  showConfirm,
  editarCategoria,
  novaCategoria,
  abrirConfirmacao,
  confirmarExclusao
} = useCategorias()

const toastRef = ref(null)
const loadingRef = ref(null)

// Carregar dados ao montar
await carregarCategorias()

async function salvar() {
  loadingRef.value?.mostrar()
  try {
    await salvarCategoria()
    toastRef.value.addMensagem('Categoria salva com sucesso!', 'success')
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
    <h1 class="mb-3">Gerenciamento de Categorias</h1>
    <button class="btn btn-primary mb-3" @click="() => { novaCategoria(); showModal = true }">
      Nova Categoria
    </button>

    <table class="table table-striped table-bordered">
      <thead class="table-dark">
        <tr>
          <th>Nome</th>
          <th>Descrição</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="c in categorias" :key="c.id">
          <td>{{ c.nome }}</td>
          <td>{{ c.descricao }}</td>
           <td>
            <button class="btn btn-sm btn-warning me-2" @click="() => { editarCategoria(c); showModal = true }">
              Editar
            </button>
            <button class="btn btn-sm btn-danger" @click="abrirConfirmacao(c.id)">
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
                {{ categoriaEditando.id ? 'Editar Categoria' : 'Nova Categoria' }}
              </h5>
              <button type="button" class="btn-close" @click="showModal = false"></button>
            </div>
            <div class="modal-body">
              <div class="mb-3">
                <label class="form-label">Nome da Categoria</label>
                <input
                  v-model="categoriaEditando.nome"
                  class="form-control"
                  :class="{ 'is-invalid': categoriaEditando.nome.length > 0 && categoriaEditando.nome.length < 5 }"
                  placeholder="Nome da categoria"
                  required
                />
                <div class="invalid-feedback">
                  O nome deve ter pelo menos 5 caracteres.
                </div>
              </div>
                <!-- Descrição -->
              <div class="mb-3">
                <label class="form-label">Descrição</label>
                <input v-model="categoriaEditando.descricao" class="form-control" placeholder="Descrição da categoria" />
              </div>
            </div>
             <div class="modal-footer">
              <button class="btn btn-success" type="submit" :disabled="categoriaEditando.nome.length < 5 || carregando">
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
            <p>Tem certeza que deseja excluir esta categoria?</p>
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
